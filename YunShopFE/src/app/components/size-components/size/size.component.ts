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
    id: 0,
    userId: 0
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
      size: this.size.id,
      userId: this.tokenService.getUserIdByToken()
    };
  }

  deleteSize(): void {
    this.createDeleteSizeRequest();
    console.log('Deleting Size:', this.deleteSizeRequest);
    this.sizeService.deleteSize(this.deleteSizeRequest).subscribe({
      next: (response: any) => {
        console.log('Size deleted:', response);
        window.location.reload();
      },
      error: (error: any) => {
        console.error('Error deleting Size:', error);
      }
    });
  }
}
