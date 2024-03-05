import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SaleService } from 'src/app/Services/sale.service';
import { Sales } from 'src/app/Models/SaleModel';
import { Router } from '@angular/router';
import { WorkBook, utils, writeFile } from 'xlsx';
import { saveAs } from 'file-saver';
import *  as xlsx from'xlsx';
@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.scss']
})
export class SaleComponent {
  sales: Sales[] = [];
  isAdding: boolean = false;
  isEditing: boolean = false;
  selectedSale: Sales | undefined;
  saleForm: FormGroup;

  constructor(private fb: FormBuilder, private saleService: SaleService, public router: Router) {
    this.saleForm = this.fb.group({
      saleDate: [''],
      totalAmount: [''],
      paymentMode: [''],
      paymentDetails: [''],
    });
  }

  ngOnInit(): void {
    this.getSales();
  }

  getSales(): void {
    this.saleService.getSales().subscribe(
      (data: Sales[]) => {
        this.sales = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onAddClick(): void {
    this.isAdding = true;
    // Reset sale form
    this.saleForm.reset();
  }

  onCancelClick(): void {
    this.isAdding = false;
    this.isEditing = false;
    this.selectedSale = undefined;
    this.saleForm.reset();
  }

  onFormSubmit(): void {
    if (this.saleForm.valid) {
      const sale: Sales = {
        salesId: this.selectedSale?.salesId || 0,
        saleDate: this.saleForm.value.saleDate,
        totalAmount: this.saleForm.value.totalAmount,
        paymentMode: this.saleForm.value.paymentMode,
        paymentDetails: this.saleForm.value.paymentDetails,
      };

      if (this.isAdding) {
        this.addSale(sale);
      } else if (this.isEditing) {
        this.updateSale(sale);
        this.router.navigate(['/sales'])
      }
    }
  }

  addSale(sale: Sales): void {
    this.saleService.addSales(sale).subscribe(
      (data: Sales) => {
        this.sales.push(data);
        this.onCancelClick();
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  onEditClick(sale: Sales): void {
    this.isEditing = true;
    this.selectedSale = { ...sale };
    this.saleForm.patchValue({
      saleDate: sale.saleDate,
      totalAmount: sale.totalAmount,
      paymentMode: sale.paymentMode,
      paymentDetails: sale.paymentDetails,
    });
  }

  updateSale(sale: Sales): void {
    if (this.selectedSale) {
      this.saleService.updateSales(sale).subscribe(
        (data: Sales) => {
          this.isEditing = false
          this.getSales();
          const index = this.sales.findIndex(s => s?.salesId === data?.salesId);
          if (index !== -1) {
            this.sales[index] = data;
            this.onCancelClick();
          }
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  deleteSale(id: number): void {
    this.saleService.deleteSales(id).subscribe(
      () => {
        this.sales = this.sales.filter(s => s.salesId !== id);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  downloadTableAsExcel(): void {
    const wb: WorkBook = { SheetNames: [], Sheets: {} };

    const ws = utils.table_to_sheet(document.getElementById('salesTable'));

    wb.SheetNames.push('Sheet 1');
    wb.Sheets['Sheet 1'] = ws;
    xlsx.writeFile(wb, 'sales_data.xlsx')
  }
}