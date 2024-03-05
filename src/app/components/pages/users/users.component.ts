import { Component, OnInit } from '@angular/core';
import { users } from 'src/app/Models/Users';
import { UsersService } from 'src/app/Services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})

export class UsersComponent implements OnInit {
  users!: users[];

  constructor(private userService: UsersService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe((users) => {
      this.users = users;
    });
  }

  deleteClient(id: number) {
    this.userService.deleteUser(id).subscribe(
      () => {
        this.users = this.users.filter(u => u.id !== id);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}