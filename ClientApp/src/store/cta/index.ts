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
        playerGuild: '',
        repoId: '',
        joining: false,
        roles: [],
    } as CtaState,
    actions: {
        async init(context: any) {
            const { commit } = moduleActionContext(context, mod);
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/cta")
                .configureLogging(signalR.LogLevel.Information)
                .build();

            const playerName = window.localStorage.getItem('player');

            if (playerName) {
                commit.SET_PLAYER_NAME(playerName);
            }

            connection.on("youJoined", (cta: Cta) => {
                commit.ADD_CALL_TO_ARMS(cta);
            });
            
            connection.onclose(() => {
                if(confirm('Connection to the server lost. Reload page?')) {
                    window.location.reload(true);
                }
            });

            const repoIdResponse = await axios.get('/api/cta');
            const repoId = repoIdResponse.data as string;
            const localRepoId = window.localStorage.getItem('repo') as string;

            if (localRepoId) {
                if (repoId !== localRepoId) {
                    window.localStorage.removeItem(localRepoId);
                }
            }

            window.localStorage.setItem('repo', repoId);
            commit.SET_REPO_ID(repoId);

            const rolesResponse = await axios.get('/api/roles');
            const roles = rolesResponse.data as Role[];
            commit.SET_ROLES(roles);

            await connection.start();
            commit.SET_CONNECTION(connection);

            const data = window.localStorage.getItem(repoId);
            if (data) {
                const ctas = JSON.parse(data);
                commit.SET_CALL_TO_ARMS(ctas);
            }
        },
        async join(context: any, ctaId: string) {
            const { commit, state } = moduleActionContext(context, mod);

            commit.SET_JOINGING(true);

            try {
                const response = await axios.get(`/api/cta/${ctaId}`);
                const joinedCta = response.data as Cta;

                if(joinedCta === null) {
                    return;
                }

                console.log('addeding cta');
                commit.ADD_CALL_TO_ARMS(joinedCta);

                await state.connection.invoke('Join', ctaId);
            } catch (ex) {
                console.error(ex);
            } finally {
                commit.SET_JOINGING(false);
            }
        },
        async createCta(context: any, payload: {title: string, setup: string; bringHammers: boolean; extraSets: number}) {
            const { commit, state } = moduleActionContext(context, mod);
            const response = await axios.post(`/api/cta`, payload);
            commit.ADD_CALL_TO_ARMS(response.data);

            window.localStorage.setItem('ctas', JSON.stringify(state.callToArms));
        },
        async selectRole(context: any, payload: { ctaId: string, roleId: string, playerName: string}) {
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
            window.localStorage.setItem(state.repoId, JSON.stringify(state.callToArms));
        },
        SET_PLAYER_NAME(state: CtaState, name: string) {
            state.playerName = name;
        },
        SET_JOINGING(state: CtaState, joining: boolean) {
            state.joining = joining;
        },
        SET_CONNECTION(state: CtaState, connection: signalR.HubConnection) {
            state.connection = connection;
            state.connected = true;
        },
        SET_REPO_ID(state: CtaState, repo: string) {
            state.repoId = repo;
        },
        SET_ROLES(state: CtaState, roles: Role[]) {
            state.roles = roles;
        }
    }
} as const;

export default mod;
