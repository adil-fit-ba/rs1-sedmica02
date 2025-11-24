import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import {Example1Component} from './example1/example1';
import {Example2Component} from './example2/example2';
import {Example3Component} from './example3/example3';
import {FormsModule} from '@angular/forms';
import {SharedModuleModule} from './shared-module/shared-module-module';
import { Example4Component } from './example4/example4.component';

@NgModule({
  declarations: [
    App,
    Example1Component,
    Example2Component,
    Example3Component,
    Example4Component,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    SharedModuleModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
