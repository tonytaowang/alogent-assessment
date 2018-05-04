import { Component, OnInit } from '@angular/core';
import { Board } from '../models/board';
import { PostIt } from '../models/post-it';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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

    createBoard(name: string) {
        debugger;
        var headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.post<Board[]>('/api/CreateBoard', JSON.stringify(name), { headers: headers }).subscribe(data => {
            this.boards = data;
        });
    }

    deleteBoard(boardId: number) {
        debugger;
        return this.http.delete<Board[]>('/api/boards/' + boardId)
            .subscribe(data => {
                this.boards = data;
            });
    }
    addPin(boardId:number,text: string) {
        debugger;
        var headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.post<Board[]>('/api/AddPin', { boardId, text }, { headers: headers }).subscribe(data => {
            this.boards = data;
        });
    }
    viewPin(boardId: number) {
        debugger;
        var headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.post<Board[]>('/api/ViewPin', { boardId }, { headers: headers }).subscribe(data => {
            this.boards = data;
        });
    }

}
