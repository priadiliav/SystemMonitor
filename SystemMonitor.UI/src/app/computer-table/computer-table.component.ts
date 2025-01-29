import { Component, OnInit } from '@angular/core';
import { ComputerService } from '../services/computer.service';
import { ComputerDetails } from '../computer';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-computer-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './computer-table.component.html',
  styleUrls: ['./computer-table.component.css'],
})
export class ComputerTableComponent implements OnInit {
  computers: ComputerDetails[] = [];

  constructor(private computerService: ComputerService) {}

  ngOnInit(): void {
    this.computerService.startConnection();
    this.computers = this.computerService.getComputers();
  }
}
