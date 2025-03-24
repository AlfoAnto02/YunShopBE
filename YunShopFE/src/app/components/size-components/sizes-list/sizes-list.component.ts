import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SizeComponent } from '../size/size.component';
import { RouterLink } from '@angular/router';
import { Size } from '../../../models/size';
import { SizeService } from '../../../services/size.service';

@Component({
  selector: 'app-sizes-list',
  imports: [CommonModule, SizeComponent, RouterLink],
  templateUrl: './sizes-list.component.html',
  styleUrl: './sizes-list.component.scss'
})
export class SizesListComponent {
  sizes: Size[] = [];

  constructor(private SizeService: SizeService) { }

  ngOnInit(): void {
    this.loadSizes();
  }

  loadSizes(): void {
    if (this.loadSizesFromLocalStorage()) {
      console.log('Sizes loaded from localStorage:', this.sizes);
    } else {
      this.loadSizesFromDatabase();
    }
  }

  loadSizesFromLocalStorage(): boolean {
    const cachedSizes = localStorage.getItem('sizes');
    if (cachedSizes) {
      this.sizes = JSON.parse(cachedSizes);
      return true;
    }
    return false;
  }

  loadSizesFromDatabase(): void {
    this.SizeService.getSizes().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.sizes = response.result;
          localStorage.setItem('sizes', JSON.stringify(this.sizes));
          console.log('Sizes loaded from API:', this.sizes);
        } else {
          console.error('Expected an array of sizes, but got:', response);
          this.sizes = [];
        }
      },
      error: (error: any) => {
        console.error('Error loading sizes:', error);
      }
    });
  }
}
