import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Supplier } from 'src/app/Models/SupplierModel';
import { SupplierService } from 'src/app/Services/supplier.service';
import { WorkBook, utils, writeFile } from 'xlsx';
import { saveAs } from 'file-saver';
import *  as xlsx from'xlsx';
@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.scss']
})
export class SupplierComponent implements OnInit {
  suppliers: Supplier[] = [];
  isAdding: boolean = false;
  isEditing: boolean = false;
  selectedSupplier: Supplier | undefined;
  supplierForm: FormGroup;

  constructor(private fb: FormBuilder, private supplierService: SupplierService) {
    this.supplierForm = this.fb.group({
      name: [''],
      contactName: [''],
      contactEmail: [''],
      contactPhone: [''],
      address: [''],
      pricingAgreement: ['']
    });
  }

  ngOnInit(): void {
    this.getSuppliers();
  }

  getSuppliers(): void {
    this.supplierService.getSuppliers().subscribe(
      (data: Supplier[]) => {
        this.suppliers = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onAddClick(): void {
    this.isAdding = true;
  }

  onEditClick(supplier: Supplier): void {
    this.isEditing = true;
    this.selectedSupplier = { ...supplier };
    // Prefill the form fields with selectedSupplier values
    this.supplierForm.patchValue({
      name: this.selectedSupplier.name,
      contactName: this.selectedSupplier.contactName,
      contactEmail: this.selectedSupplier.contactEmail,
      contactPhone: this.selectedSupplier.contactPhone,
      address: this.selectedSupplier.address,
      pricingAgreement: this.selectedSupplier.pricingAgreement
    });
  }

  onCancelClick(): void {
    this.isAdding = false;
    this.isEditing = false;
    this.selectedSupplier = undefined;
    this.supplierForm.reset();
  }

  onFormSubmit(): void {
    if (this.supplierForm.valid) {
      const supplier: Supplier = {
        supplierId: this.selectedSupplier?.supplierId || 0,
        name: this.supplierForm.value.name,
        contactName: this.supplierForm.value.contactName,
        contactEmail: this.supplierForm.value.contactEmail,
        contactPhone: this.supplierForm.value.contactPhone,
        address: this.supplierForm.value.address,
        pricingAgreement: this.supplierForm.value.pricingAgreement
      };

      if (this.isAdding) {
        this.addSupplier(supplier);
      } else if (this.isEditing) {
        this.updateSupplier(supplier);
      }
    }
  }

  addSupplier(supplier: Supplier): void {
    this.supplierService.addSupplier(supplier).subscribe(
      (data: Supplier) => {
        this.suppliers.push(data);
        this.onCancelClick();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  updateSupplier(supplier: Supplier): void {
    if (this.selectedSupplier) {
      this.supplierService.updateSupplier(supplier).subscribe(
        (data: Supplier) => {
          const index = this.suppliers.findIndex(s => s.supplierId === data.supplierId);
          if (index !== -1) {
            this.suppliers[index] = data;
            this.onCancelClick();
          }
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  deleteSupplier(id: number): void {
    this.supplierService.deleteSupplier(id).subscribe(
      () => {
        this.suppliers = this.suppliers.filter(s => s.supplierId !== id);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  downloadTableAsExcel(): void {
    const wb: WorkBook = { SheetNames: [], Sheets: {} };

    const ws = utils.table_to_sheet(document.getElementById('supplierTable'));

    wb.SheetNames.push('Sheet 1');
    wb.Sheets['Sheet 1'] = ws;
    xlsx.writeFile(wb, 'supplier_data.xlsx')
  }
}
