import { Component, Input, OnInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Staff } from '../models/staff.models';

Chart.register(...registerables);

@Component({
  selector: 'app-staff-chart',
  imports: [],
  templateUrl: './staff-chart.component.html',
  styleUrl: './staff-chart.component.css'
})
export class StaffChartComponent implements OnInit {
  @Input() staffs: Staff[] = [];

  chart: Chart | undefined;

  ngOnInit(): void {
    this.createBarChart();
    this.createDoughnutChart();
  }
  
  createBarChart() {
    const staffRoles = this.staffs.map(staff => staff.staffRole);
    const uniqueRoles = Array.from(new Set(staffRoles));
    const roleCounts = uniqueRoles.map(role => staffRoles.filter(r => r === role).length);
  
    this.chart = new Chart('barChart', {
      type: 'bar',
      data: {
        labels: uniqueRoles,
        datasets: [{
          //label: 'Staff Count',  Removed the label property
          data: roleCounts,
          backgroundColor: [
            'rgba(255, 99, 132, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(255, 206, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(255, 159, 64, 0.5)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 1
        }]
      },
      options: {
        plugins: {
          legend: {
            display: false // Disable the legend for the bar chart
          }
        },
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }
  
  createDoughnutChart() {
    const staffRoles = this.staffs.map(staff => staff.staffRole);
    const uniqueRoles = Array.from(new Set(staffRoles));
    const roleCounts = uniqueRoles.map(role => staffRoles.filter(r => r === role).length);
  
    this.chart = new Chart('doughnutChart', { 
      type: 'doughnut',
      data: {
        labels: uniqueRoles,
        datasets: [{
          label: 'Staff Count',
          data: roleCounts,
          backgroundColor: [
            'rgba(255, 99, 132, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(255, 206, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(255, 159, 64, 0.5)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 1
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            position: 'top',
          },
          tooltip: {
            enabled: true
          }
        }
      }
    });
  }

}
