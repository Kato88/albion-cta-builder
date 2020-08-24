export interface CtaState {
  callToArms: Cta[];
  connection: signalR.HubConnection;
  connected: boolean;
  playerName: string;
  playerGuild: string;
  repoId: string;
  joining: boolean;
  roles: Role[];
}

export interface Cta {
  id: string;
  title: string;
  setup: string;
  bringHammers: boolean;
  extraSets: number;
  players: Player[];
}

export interface Role {
  id: string;
  title: string;
  category: string;
  internalName: string;
}

export interface Player {
  name: string;
  role: Role;
}

export interface QueuePlayer {
  name: string;
  roles: Role[];
}