import { Component, ElementRef, ViewChild, OnInit, TemplateRef } from '@angular/core';
import { Display } from 'src/app/Models/Display';
import { DisplayService } from 'src/app/Services/display.service';
import { Chart, ChartData, ChartOptions } from 'chart.js';
import { registerables } from 'chart.js';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  summary!: Display;
  chart: Chart | undefined;
  @ViewChild('chartCanvas1')
chartCanvasRef1!: ElementRef<HTMLCanvasElement>;
  @ViewChild('dialog')
  dialogTemplate!: TemplateRef<any>;
@ViewChild('chartCanvas2')
chartCanvasRef2!: ElementRef<HTMLCanvasElement>;
  chartData1: ChartData = {
    labels: ['Sales', 'Purchase'],
    datasets: [
      {
        label: 'Quantity',
        data: [50, 30, 70, 40, 60, 50, 80],
        backgroundColor: 'rgba(75, 192, 192, 0.2)',
        borderColor: 'rgba(75, 192, 192, 1)',
        borderWidth: 1,
      },
    ],
  };
  chartData2: ChartData = {
    labels: ['Sales', 'Purchase'],
    datasets: [
      {
        label: 'Amount',
        data: [50, 30, 70, 40, 60, 50, 80],
        backgroundColor: 'rgba(75, 192, 192, 0.2)',
        borderColor: 'rgba(75, 192, 192, 1)',
        borderWidth: 1,
      },
    ],
  };
  chartOptions: ChartOptions = {
    responsive: true,
  };
  showChartFlag1 = false;
  showChartFlag2 = false;

  constructor(private dataService: DisplayService, public dialogService: MatDialog ) {}

  ngOnInit(): void {
    this.getDataSummary();
  }

  getDataSummary(): void {
    this.dataService.getSummary().subscribe((summary) => {
      this.summary = summary;
      // setTimeout(() => {
        if (this.summary.totalPurchasesQuantity<=200){
          this.dialogService.open(this.dialogTemplate)
        }
      // }, 0);
    });
  }

  updateChartData1(summary: Display): void {
    if (this.chartData1.datasets) {
      this.chartData1.datasets[0].data = [
        summary.totalSalesQuantity,
        summary.totalPurchasesQuantity,
      ];
    }
  }
  updateChartData2(summary: Display): void {
    if (this.chartData2.datasets) {
      this.chartData2.datasets[0].data = [
        summary.totalSalesAmount,
        summary.totalPurchaseAmount,
      ];
    }
  }
  showChart1(): void {
    this.showChartFlag1 = !this.showChartFlag1;
    if (this.showChartFlag1) {
      this.updateChartData1(this.summary);
      setTimeout(() => {
        this.renderChart1();
      }, 0);
    } else {
      setTimeout(()=>{this.closeChart1();},0)
    }
  }

  closeChart1(): void {
    this.showChartFlag1 = false;
    if (this.chart) {
      this.chart.destroy();
    }
  }

  showChart2(): void {
    this.showChartFlag2 = !this.showChartFlag2;
    if (this.showChartFlag2) {
      this.updateChartData2(this.summary);
      setTimeout(() => {
        this.renderChart2();
      }, 0);
    } else {
      setTimeout(()=>{this.closeChart2();},0)
    }
  }

  closeChart2(): void {
    this.showChartFlag2 = false;
    if (this.chart) {
      this.chart.destroy();
    }
  }

  renderChart1(): void {
    const canvas = this.chartCanvasRef1.nativeElement;
    if (canvas) {
      const ctx = canvas.getContext('2d');
      if (ctx) {
        Chart.register(...registerables);
        this.chart = new Chart(ctx, {
          type: 'bar',
          data: this.chartData1,
          options: this.chartOptions,
        });
      }
    }
  }
  renderChart2(): void {
    const canvas = this.chartCanvasRef2.nativeElement;
    if (canvas) {
      const ctx = canvas.getContext('2d');
      if (ctx) {
        Chart.register(...registerables);
        this.chart = new Chart(ctx, {
          type: 'bar',
          data: this.chartData2,
          options: this.chartOptions,
        });
      }
    }
  }
}
