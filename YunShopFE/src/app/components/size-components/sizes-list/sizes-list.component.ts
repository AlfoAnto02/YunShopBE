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
    this.SizeService.getSizes().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.sizes = response.result;
          console.log('Sizes loaded:', this.sizes);
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
