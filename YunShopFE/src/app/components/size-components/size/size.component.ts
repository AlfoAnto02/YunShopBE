import { Component, Input } from '@angular/core';
import { Size } from '../../../models/size';
import { UserService } from '../../../services/user.service';
import { TokenService } from '../../../services/token.service';
import { SizeService } from '../../../services/size.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-size',
  imports: [CommonModule],
  templateUrl: './size.component.html',
  styleUrl: './size.component.scss'
})
export class SizeComponent {
  @Input() size!: Size;
  addedByUsername: string = '';

  deleteSizeRequest: any = {
    sizeId: 0
  };

  constructor(private userService: UserService, private tokenService: TokenService,
    private sizeService: SizeService) { }

  ngOnInit(): void {
    this.getAddedByUsername();
  }

  getAddedByUsername(): void {
    this.userService.getUserById(this.size.addedBy).subscribe({
      next: (response: any) => {
        this.addedByUsername = response.result.user.userName;
      },
      error: (error: any) => {
        console.error('Error loading user:', error);
      }
    });
  }

  createDeleteSizeRequest(): void {
    this.deleteSizeRequest = {
      sizeId: this.size.id
    };
  }

  deleteSize(): void {
      this.createDeleteSizeRequest();
      console.log('Deleting Size:', this.deleteSizeRequest);
      this.sizeService.deleteSize(this.deleteSizeRequest).subscribe({
        next: (response: any) => {
          console.log('Size deleted:', response);
          
          // Remove the size from localStorage
          const cachedSizes = localStorage.getItem('sizes');
          if (cachedSizes) {
            let sizes = JSON.parse(cachedSizes);
            sizes = sizes.filter((size: Size) => size.id !== this.size.id);
            localStorage.setItem('sizes', JSON.stringify(sizes));
          }
          
          window.location.reload();
        },
        error: (error: any) => {
          console.error('Error deleting Size:', error);
        }
      });
    }
}
