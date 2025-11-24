import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ShowError} from './shared-module/show-error/show-error';
import {Example1Component} from './example1/example1';
import {Example3Component} from './example3/example3';
import {Example2Component} from './example2/example2';
import {Example4Component} from './example4/example4.component';



const routes: Routes = [
  { path: 'example1', component: Example1Component},
  { path: 'example2', component: Example2Component },
  { path: 'example3', component: Example3Component },
  { path: 'example4', component: Example4Component },
  { path: 'error-page', component: ShowError },

  // default rute
  { path: '', redirectTo: 'komponenta1', pathMatch: 'full' },

  // ako URL ne postoji
  { path: '**', redirectTo: 'error-page' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
