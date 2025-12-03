import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Example1Component } from './example1/example1.component';
import { Example3Component } from './example3/example3.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import {FormsModule} from '@angular/forms';
import { Example2SignalComponent } from './example2-signal/example2-signal.component';
import { Example2ManualChangeDetectionComponent } from './example2-manualchangedetection/example2-manualchangedetection.component';


@NgModule({
  declarations: [
    App,
    Example1Component,
    Example2SignalComponent,
    Example2ManualChangeDetectionComponent,
    Example3Component,
    ErrorPageComponent,
    Example2SignalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule // za ngModel
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
