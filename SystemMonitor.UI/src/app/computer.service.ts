import { Injectable } from '@angular/core';
import { ComputerDetails } from './computer';
import * as signalR from '@microsoft/signalr';

@Injectable({ providedIn: 'root' })
export class ComputerService {
  private hubConnection!: signalR.HubConnection;
  private Computers: ComputerDetails[] = [];

  constructor() {}

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5183/computerHub')
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
      console.log('Received computer details: ', data);

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
