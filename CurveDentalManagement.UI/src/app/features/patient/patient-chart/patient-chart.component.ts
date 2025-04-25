import { Component, Input, OnInit } from '@angular/core';
import { Patient } from '../models/patient.models';
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-patient-chart',
  imports: [],
  templateUrl: './patient-chart.component.html',
  styleUrl: './patient-chart.component.css'
})
export class PatientChartComponent implements OnInit {
  @Input() patients: Patient[] = [];
  barChart: Chart | undefined;
  doughnutChart: Chart | undefined;

  ngOnInit(): void {
    this.createBarChart();
    this.createDoughnutChart();
  }

  createBarChart() {
    const ageGroups = ['0-18', '19-35', '36-50', '51+'];
    const ageCounts = [0, 0, 0, 0];

    this.patients.forEach(patient => {
      const age = Number(patient.age);
      if (age <= 18) ageCounts[0]++;
      else if (age <= 35) ageCounts[1]++;
      else if (age <= 50) ageCounts[2]++;
      else ageCounts[3]++;
    });

    this.barChart = new Chart('barChart', {
      type: 'bar',
      data: {
        labels: ageGroups,
        datasets: [
          {
            label: 'Age Distribution',
            data: ageCounts,
            backgroundColor: [
              'rgba(255, 99, 132, 0.5)',
              'rgba(54, 162, 235, 0.5)',
              'rgba(255, 206, 86, 0.5)',
              'rgba(75, 192, 192, 0.5)'
            ],
            borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
              'rgba(255, 206, 86, 1)',
              'rgba(75, 192, 192, 1)'
            ],
            borderWidth: 1
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            display: true
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
    const genderCounts = { Male: 0, Female: 0, Other: 0 };

    this.patients.forEach(patient => {
      if (patient.gender === 'Male') genderCounts.Male++;
      else if (patient.gender === 'Female') genderCounts.Female++;
      else genderCounts.Other++;
    });

    this.doughnutChart = new Chart('doughnutChart', {
      type: 'doughnut',
      data: {
        labels: ['Male', 'Female'],
        datasets: [
          {
            label: 'Gender Distribution',
            data: Object.values(genderCounts),
            backgroundColor: [
              'rgba(54, 162, 235, 0.5)',
              'rgba(255, 99, 132, 0.5)',
              'rgba(153, 102, 255, 0.5)'
            ],
            borderColor: [
              'rgba(54, 162, 235, 1)',
              'rgba(255, 99, 132, 1)',
              'rgba(153, 102, 255, 1)'
            ],
            borderWidth: 1
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            position: 'top'
          }
        }
      }
    });
  }
}