import { HttpClient } from '@angular/common/http';
import {SelectionModel} from '@angular/cdk/collections';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

export interface Log{
  logID: number,
  application: string,
  applicationVersion: string,
  customerId: number,
  companyId: number,
  logDateTime: string,
  logContent: string
}

const TABLE_PLACEHOLDER: Log[] = []

@Component({
  selector: 'log-display-sticky',
  templateUrl: './log-display-sticky.component.html',
  styleUrls: ['./log-display-sticky.component.css']
})
export class LogDisplayStickyComponent implements OnInit {
  displayedColumns: string[] = ['select','logID', 'application', 'applicationVersion', 'customerId', 'companyId','logDateTime','logContent','actions'];
  dataSource = new MatTableDataSource<Log>([]);
  bogusDataSource = new MatTableDataSource<Log >(TABLE_PLACEHOLDER);
  selection = new SelectionModel<Log>(true, []);


  @ViewChild(MatPaginator, {static: true}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort!: MatSort;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchLogs()
  }

  fetchLogs(): void {
    this.http.get('https://api.jsonbin.io/b/6250885bd20ace068f959856/9').subscribe((data) => {
      this.dataSource.data = data as Log[];
    })
  }

    //PAGINATION
    ngAfterViewInit(): void {   
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  
    applyFilter(event: Event) {
      const filterValue = (event.target as HTMLInputElement).value;
      this.dataSource.filter = filterValue.trim().toLowerCase();
  
      if (this.dataSource.paginator) {
        this.dataSource.paginator.firstPage();
      }
    }

    isAllSelected() {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSource.data.length;
      return numSelected === numRows;
    }
  
    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
      if (this.isAllSelected()) {
        this.selection.clear();
        return;
      }
      this.selection.select(...this.dataSource.data);
    }
  
    /** The label for the checkbox on the passed row */
    checkboxLabel(row?: Log): string {
      if (!row) {
        return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
      }
      return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.logID + 1}`;
    }
  

}
