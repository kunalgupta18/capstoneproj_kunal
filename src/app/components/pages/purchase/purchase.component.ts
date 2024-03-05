import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Purchase } from 'src/app/Models/PurchaseModel';
import { PurchaseService } from 'src/app/Services/purchase.service';
import { WorkBook, utils } from 'xlsx';
import * as xlsx from 'xlsx';


@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss']
})
export class PurchaseComponent implements OnInit {
  purchases!: Purchase[];
  purchase1!:Purchase;
  purchaseForm: FormGroup;
  isAdding: boolean = false;
  isEditing: boolean = false;
  selectedPurchase: Purchase | undefined;

  constructor(private fb: FormBuilder, private purchaseService: PurchaseService) {
    this.purchaseForm = this.fb.group({
      purchaseDate: [''],
      supplierId: [''],
      quantity: [''],
      totalAmount: ['']
    });
  }

  ngOnInit(): void {
    this.loadPurchases();
  }

  loadPurchases(): void {
    this.purchaseService.getAllPurchases().subscribe(purchases => {
      this.purchases = purchases;
    });
  }

  addPurchase(): void {
    if (this.purchaseForm.valid) {
      const purchase: Purchase = {
        purchaseId: 0,
        purchaseDate: this.purchaseForm.value.purchaseDate,
        supplierId: this.purchaseForm.value.supplierId,
        quantity: this.purchaseForm.value.quantity,
        totalAmount: this.purchaseForm.value.totalAmount
      };

      this.purchaseService.addPurchase(purchase).subscribe(() => {
        this.loadPurchases();
        this.purchaseForm.reset();
      });
    }
    this.isAdding=false;
  }

  editPurchase(purchase: Purchase): void {
    this.selectedPurchase = { ...purchase };
    this.purchaseForm.patchValue({
      purchaseDate: this.selectedPurchase.purchaseDate,
      supplierId: this.selectedPurchase.supplierId,
      quantity: this.selectedPurchase.quantity,
      totalAmount: this.selectedPurchase.totalAmount
    });
    this.isEditing = true;
  }

  updatePurchase(): void {
    if (this.purchaseForm.valid && this.selectedPurchase) {
      const updatedPurchase: Purchase = {
        ...this.selectedPurchase,
        purchaseDate: this.purchaseForm.value.purchaseDate,
        supplierId: this.purchaseForm.value.supplierId,
        quantity: this.purchaseForm.value.quantity,
        totalAmount: this.purchaseForm.value.totalAmount
      };

      this.purchaseService.updatePurchase(updatedPurchase).subscribe(() => {
        this.loadPurchases();
        this.cancelEdit();
      });
    }
  }

  deletePurchase(purchaseId: number): void {
    this.purchaseService.deletePurchase(purchaseId).subscribe(() => {
      this.loadPurchases();
    });
  }

  cancelEdit(): void {
    this.isEditing = false;
    this.isAdding = false;
    this.selectedPurchase = undefined;
    this.purchaseForm.reset();
  }
  downloadTableAsExcel(): void {
  console.log("Called")
    const wb: WorkBook = { SheetNames: [], Sheets: {} };

    const ws = utils.table_to_sheet(document.getElementById('purchaseTable'));

    wb.SheetNames.push('Sheet 1');
    wb.Sheets['Sheet 1'] = ws;
    xlsx.writeFile(wb, 'purchase_data.xlsx')
  }
}
