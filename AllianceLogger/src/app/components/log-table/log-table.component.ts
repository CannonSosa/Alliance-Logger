import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { HttpClient } from '@angular/common/http';

export interface Log{
    logID: number,
    application: string,
    applicationVersion: string,
    customerID: number,
    logDateTime: string,
    logContent: string
}

@Component({
  selector: 'log-table',
  templateUrl: './log-table.component.html',
  styleUrls: ['./log-table.component.css']
})
export class LogTableComponent implements OnInit {
  displayedColumns: string[] = ['logID', 'application', 'applicationVersion', 'customerID', 'logDateTime', 'LogContent', 'actions'];
  dataSource = new MatTableDataSource<Log>([]);
  @ViewChild(MatPaginator, {static: true}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort!: MatSort;

  constructor(private http: HttpClient) { 

  }

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.fetchLogs()
    console.log('DataSource', this.dataSource);
  }

  fetchLogs(): void {
    this.http.get('https://api.jsonbin.io/b/6250885bd20ace068f959856/3').subscribe((data) => {
      this.dataSource.data = data as Log[];
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
