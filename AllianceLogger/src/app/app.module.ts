import { LogsService } from './logs.service';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import {MatCheckboxModule} from '@angular/material/checkbox'; 

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { LogTableComponent } from './components/log-table/log-table.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { LoginCardComponent } from './components/login-card/login-card.component';
import { LogDashboardComponent } from './pages/log-dashboard/log-dashboard.component';
import { FilterDropdownComponent } from './components/filter-dropdown/filter-dropdown.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { LogDisplayComponent } from './components/log-display/log-display.component';
import { LogDisplayStickyComponent } from './components/log-display-sticky/log-display-sticky.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    LoginCardComponent,
    LogTableComponent,
    LogDashboardComponent,
    SidebarComponent,
    FilterDropdownComponent,
    LogDisplayComponent,
    LogDisplayStickyComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatTableModule,
    MatInputModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule
  ],
  providers: [
    LogsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
