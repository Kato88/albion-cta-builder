export interface CtaState {
  callToArms: Cta[];
  connection: signalR.HubConnection;
  connected: boolean;
}

export interface Cta {
  id: string;
  title: string;
  roles: Role[];
}

export interface Role {
  title: string;
  category: string;
  players: Player[];
}

export interface Player {
  name: string;
}
