export interface ComputerDetails {
  id?: number;
  name: string;
  metrics: ComputerMetrics;
}

export interface ComputerMetrics {
  id?: number;
  cpuUsage: number;
  diskUsage: number;
  ramUsage: number;
  networkUsage: number;
}
