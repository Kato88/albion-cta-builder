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
        playerName: '',
    } as CtaState,
    actions: {
        async init(context: any) {
            const { commit } = moduleActionContext(context, mod);
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/cta")
                .configureLogging(signalR.LogLevel.Information)
                .build();

            const playerName = window.localStorage.getItem('playerName');

            if (playerName) {
                commit.SET_PLAYER_NAME(playerName);
            }

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

            const data = window.localStorage.getItem('ctas');
            if (data) {
                const ctas = JSON.parse(data);
                commit.SET_CALL_TO_ARMS(ctas);
            }
        },
        async join(context: any, ctaId: string) {
            const { commit, state } = moduleActionContext(context, mod);

            const response = await axios.get(`/api/cta/${ctaId}`);
            const joinedCta = response.data as Cta;

            console.log('addeding cta');
            commit.ADD_CALL_TO_ARMS(joinedCta);

            state.connection.invoke('Join', ctaId);
        },
        async createCta(context: any, title: string) {
            const { commit, state } = moduleActionContext(context, mod);
            const response = await axios.post(`/api/cta`, { title: title });
            commit.ADD_CALL_TO_ARMS(response.data);

            window.localStorage.setItem('ctas', JSON.stringify(state.callToArms));
        },
        async selectRole(context: any, payload: { ctaId: string, roleId: string, playerName: string }) {
            const { commit, state } = moduleActionContext(context, mod);

            await axios.patch(`/api/cta`, {
                CtaId: payload.ctaId,
                RoleId: payload.roleId,
                Player: payload.playerName
            });
        }
    },
    mutations: {
        SET_CALL_TO_ARMS(state: CtaState, ctas: Cta[]) {
            state.callToArms = ctas;
        },
        ADD_CALL_TO_ARMS(state: CtaState, cta: Cta) {
            const asd = state.callToArms;

            const index = asd.findIndex((x) => x.id === cta.id);
            if (index === -1) {
                asd.push(cta);
            } else {
                asd.splice(index, 1, cta);
            }

            state.callToArms = asd;
            window.localStorage.setItem('ctas', JSON.stringify(state.callToArms));
        },
        SET_PLAYER_NAME(state: CtaState, name: string) {
            state.playerName = name;
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
