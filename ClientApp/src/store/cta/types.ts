export interface CtaState {
  callToArms: Cta[];
  connection: signalR.HubConnection;
  connected: boolean;
  playerName: string;
  repoId: string;
  joining: boolean;
}

export interface Cta {
  id: string;
  title: string;
  roles: Role[];
}

export interface Role {
  id: string;
  title: string;
  category: string;
  players: Player[];
}

export interface Player {
  name: string;
}
