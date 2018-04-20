import { Component, OnInit } from '@angular/core';
import { Board } from '../models/board';
import { PostIt } from '../models/post-it';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  boards: Board[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<Board[]>('/api/boards').subscribe(data => {
      this.boards = data;
    });
  }
}
