import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Product } from 'src/app/Models/ProductModel';
import { ProductService } from 'src/app/Services/product.service';
import { WorkBook, utils } from 'xlsx';
import * as xlsx from 'xlsx';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  isAdding: boolean = false;
  isEditing: boolean = false;
  selectedProduct: Product | undefined;
  productForm!: FormGroup;

  constructor(
    private productService: ProductService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.loadProducts();
    this.initForm();
  }

  initForm() {
    this.productForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      description: ['', Validators.required],
      sku: ['', Validators.required],
      category: ['', Validators.required],
      manufacturer: ['', Validators.required],
      price: [0, Validators.required],
      quantity: [0, Validators.required],
    });
  }

  loadProducts() {
    this.productService.getAllProducts().subscribe(
      (data: Product[]) => {
        this.products = data;
        this.filteredProducts = [...this.products];
      },
      (error) => {
        console.log('An error occurred while fetching products:', error);
      }
    );
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();

    this.filteredProducts = this.products.filter(
      (product) =>
        product.name.toLowerCase().includes(filterValue) ||
        product.category.toLowerCase().includes(filterValue) ||
        product.sku.toLowerCase().includes(filterValue)
    );
  }

  onAddClick() {
    this.isAdding = true;
    this.isEditing = false;
    this.clearForm();
  }

  onEditClick(product: Product) {
    this.isAdding = false;
    this.isEditing = true;
    this.selectedProduct = product;
    this.productForm.patchValue(product);
  }

  onCancelClick() {
    this.isAdding = false;
    this.isEditing = false;
    this.clearForm();
  }

  onFormSubmit() {
    if (this.productForm.valid) {
      const product: Product = {
        id: this.selectedProduct?.id || 0,
        name: this.productForm.value.name,
        description: this.productForm.value.description,
        sku: this.productForm.value.sku,
        category: this.productForm.value.category,
        manufacturer: this.productForm.value.manufacturer,
        price: this.productForm.value.price,
        quantity: this.productForm.value.quantity,
      };

      if (this.isAdding) {
        this.addProduct(product);
      } else if (this.isEditing) {
        this.updateProduct(product);
      }
    }
  }

  addProduct(product: Product) {
    this.productService.addProduct(product).subscribe(
      (data: Product) => {
        this.products.push(data);
        this.clearForm();
        this.isAdding = false;
      },
      (error) => {
        console.log('An error occurred while adding the product:', error);
      }
    );
  }

  updateProduct(product: Product) {
    this.productService.updateProduct(product).subscribe(
      (data: Product) => {
        const index = this.products.findIndex((p) => p.id === data.id);
        if (index !== -1) {
          this.products[index] = data;
        }
        this.clearForm();
        this.isEditing = false;
      },
      (error) => {
        console.log('An error occurred while updating the product:', error);
      }
    );
  }

  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe(
      () => {
        this.products = this.products.filter((p) => p.id !== id);
        this.clearForm();
        this.filteredProducts = this.products;
      },
      (error) => {
        console.log('An error occurred while deleting the product:', error);
      }
    );
  }

  clearForm() {
    this.productForm.reset();
  }

  downloadTableAsExcel(): void {
    var wb = xlsx.utils.book_new();
    var ws = xlsx.utils.json_to_sheet([...this.filteredProducts]);
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet 1');
    xlsx.writeFile(wb, `products_data.xlsx`);

  }
}
