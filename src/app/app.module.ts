import { PostLogsService } from './Services/post-logs.service';


import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTabsModule} from '@angular/material/tabs';

import { LoginPageComponent } from './pages/login-page/login-page.component';
import { LoginCardComponent } from './components/login-card/login-card.component';
import { LogDashboardComponent } from './pages/log-dashboard/log-dashboard.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { LogDisplayComponent } from './components/log-display/log-display.component';
import { LogTableComponent } from './components/log-table/log-table.component';
import { FooterComponent } from './components/footer/footer.component';
import { InfoDisplayComponent } from './components/info-display/info-display.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    LoginCardComponent,
    LogDashboardComponent,
    SidebarComponent,
    LogDisplayComponent,
    LogTableComponent,
    FooterComponent,
    InfoDisplayComponent
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
    MatCheckboxModule,
    MatToolbarModule,
    MatTabsModule
  ],
  providers: [
    PostLogsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
