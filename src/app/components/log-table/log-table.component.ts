import { PostLogsService } from './../../Services/post-logs.service';
import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';

export interface Log{
  LogID: number,
  Application: string,
  ApplicationVersion: string,
  CustomerID: number,
  CompanyID: number,
  LogDateTime: string,
  LogContent: string,
  Notes: string
}

@Component({
  selector: 'log-table',
  templateUrl: './log-table.component.html',
  styleUrls: ['./log-table.component.css']
})
export class LogTableComponent implements OnInit, AfterViewInit {
  color = "black";
  logTabs: string[] = ['dashboard', 'Bookmarked','Recent'];

  displayedColumns: string[] = ['Select','LogID','Application', 'ApplicationVersion', 'CustomerID', 'CompanyID','LogDateTime','LogContent','Actions'];
  dataSource = new MatTableDataSource<Log>([]);
  clickedRows = new Set<Log>();
  bookmarked: number[] = [];
  selection = new SelectionModel<Log>(true, []);

  @ViewChild(MatPaginator, {static: true}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort!: MatSort;

  constructor(private service: PostLogsService ) {}

  ngOnInit(): void {
    this.fetchLogs()
  }

  fetchLogs(): void {
    this.service.getLogs().subscribe((data) => {
      this.dataSource.data = data as Log[];
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }


  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.displayedColumns, event.previousIndex, event.currentIndex);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  //SELECT
  /** Whether the number of selected elements matches the total number of rows. */
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
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.LogID + 1}`;
  }




}
