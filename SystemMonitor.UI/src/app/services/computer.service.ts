import { Injectable } from '@angular/core';
import { ComputerDetails } from '../models/computer';
import * as signalR from '@microsoft/signalr';

@Injectable({ providedIn: 'root' })
export class ComputerService {
  private hubConnection!: signalR.HubConnection;
  private Computers: ComputerDetails[] = [];

  constructor() {}

  public startConnection(): void {
    const jwtToken =
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ2aXRhbGlpcCIsImp0aSI6ImViOTFmNTU3LTAwMWUtNDI1My05ZjViLTZkNDkyYjEwODM1NiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzM4MTc4NDMyLCJpc3MiOiJTeXN0ZW1Nb25pdG9yLkF1dGgiLCJhdWQiOiJTeXN0ZW1Nb25pdG9yLlNlcnZpY2VzIn0.fQhyzJrE4VAjh2RuXwf7hdOqQfgqMOv7viKl25u8AHA';

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5183/computerHub', {
        accessTokenFactory: () => jwtToken,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR connection established.');
        this.registerOnServerEvents();
      })
      .catch((err) => console.error('SignalR connection error: ', err));
  }

  private registerOnServerEvents(): void {
    this.hubConnection.on('ReceiveComputerDetails', (data: ComputerDetails) => {
      this.updateComputers(data);
    });
  }

  private updateComputers(computer: ComputerDetails): void {
    const index = this.Computers.findIndex((c) => c.name === computer.name);
    if (index >= 0) {
      this.Computers[index] = computer;
    } else {
      this.Computers.push(computer);
    }
  }

  public getComputers(): ComputerDetails[] {
    return this.Computers;
  }
}
