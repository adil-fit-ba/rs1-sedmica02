import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderMenu } from './header-menu/header-menu';
import { ShowError } from './show-error/show-error';
import {RouterLink} from '@angular/router';



@NgModule({
  declarations: [


    HeaderMenu,
        ShowError
  ],
  exports: [
    HeaderMenu
  ],
  imports: [
    CommonModule,
    RouterLink
  ]
})
export class SharedModuleModule { }
