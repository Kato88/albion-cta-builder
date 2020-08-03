import store, { moduleActionContext } from "..";
import { CtaState, Cta, Role } from "./types";
import * as signalR from "@microsoft/signalr";
import axios from 'axios';

const mod = {
    namespace: true,
    state: {
        callToArms: [],
        connection: {} as signalR.HubConnection,
        connected: false,
    } as CtaState,
    actions: {
        async init(context: any) {
            const { commit } = moduleActionContext(context, mod);
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/cta")
                .configureLogging(signalR.LogLevel.Information)
                .build();


            connection.on("CtaAdded", (cta: Cta) => {
                console.log("new cta", cta);
                commit.ADD_CALL_TO_ARMS(cta);
            });

            connection.on("youJoined", (cta: Cta) => {
                console.log("you Joined", cta);
                commit.ADD_CALL_TO_ARMS(cta);
            });

            connection.on("RoleChanged", (ctaId: string, role: Role) => {
                console.log("role changed", role);
                commit.UPDATE_ROLE({
                    id: ctaId,
                    role: role
                });
            });

            await connection.start();
            commit.SET_CONNECTION(connection);
            
            const response = await axios.get('/api/cta');
            commit.SET_CALL_TO_ARMS(response.data);
        },
        async join(context: any, ctaId: string) {
            const { commit, state } = moduleActionContext(context, mod);

            const response = await axios.get(`/api/cta/${ctaId}`);
            const joinedCta = response.data as Cta;
            
            if (state.callToArms.find((x) => x.id === joinedCta.id) === null) {
                commit.ADD_CALL_TO_ARMS(joinedCta);
            }
        },
        async createCta(context: any, title: string) {
            const { commit, state } = moduleActionContext(context, mod);
            await axios.post(`/api/cta`, { title: title });
        }
    },
    mutations: {
        SET_CALL_TO_ARMS(state: CtaState, ctas: Cta[]) {
            state.callToArms = ctas;
        },
        ADD_CALL_TO_ARMS(state: CtaState, cta: Cta) {
            const asd = state.callToArms;
            asd.push(cta);
            state.callToArms = asd;
        },
        UPDATE_ROLE(state: CtaState, payload: { id: string; role: Role }) {
            const ctaIndex = state.callToArms.findIndex(x => x.id === payload.id);

            if (ctaIndex === -1) {
                return;
            }

            const cta = state.callToArms[ctaIndex];

            const roleIndex = cta.roles.findIndex(
                x =>
                    x.title === payload.role.title && x.category === payload.role.category
            );

            if (roleIndex === -1) {
                return;
            }

            cta.roles.splice(roleIndex, 1, payload.role);
            state.callToArms.splice(ctaIndex, 1, cta);
        },
        SET_CONNECTION(state: CtaState, connection: signalR.HubConnection) {
            state.connection = connection;
            state.connected = true;
        }
    }
} as const;

export default mod;
