import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { HttpClient } from '@angular/common/http';
import {SelectionModel} from '@angular/cdk/collections';
import {MatCheckboxModule} from '@angular/material/checkbox'; 

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
  displayedColumns: string[] = ['select','logID', 'application', 'applicationVersion', 'customerID', 'logDateTime', 'LogContent', 'actions'];
  dataSource = new MatTableDataSource<Log>([]);
  selection = new SelectionModel<Log>(true, []);
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
    this.http.get('https://api.jsonbin.io/b/6250885bd20ace068f959856/10').subscribe((data) => {
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

  //CHECKBOX
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  checkboxLabel(row?: Log): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.logID + 1}`;
  }
  


}
