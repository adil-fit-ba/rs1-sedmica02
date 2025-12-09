import {NgModule, provideBrowserGlobalErrorListeners, provideZoneChangeDetection} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Example1Component } from './example1/example1.component';
import { Example3Component } from './example3/example3.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { Example2SignalComponent } from './example2-signal/example2-signal.component';
import { Example2ManualChangeDetectionComponent } from './example2-manualchangedetection/example2-manualchangedetection.component';
import { Example4Component } from './example4/example4.component';
import { Example5Component } from './example5/example5.component';


@NgModule({
  declarations: [
    App,
    Example1Component,
    Example2SignalComponent,
    Example2ManualChangeDetectionComponent,
    Example3Component,
    ErrorPageComponent,
    Example2SignalComponent,
    Example4Component,
    Example5Component,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule // za ngModel
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection()
  ],
  bootstrap: [App]
})
export class AppModule { }
