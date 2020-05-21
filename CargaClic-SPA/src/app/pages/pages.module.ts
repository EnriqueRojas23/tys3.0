import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PAGES_ROUTES } from './pages.routes';
import { CommonModule } from '@angular/common';
import { AuthGuard } from '../_guards/auth.guard';
import { ListausuariosComponent, NgbdModalConfirmAutofocus, DialogOverviewExampleDialog } from './seguridad/usuario/listausuarios/listausuarios.component';
import { MatTableModule ,MatButtonModule, MatPaginatorModule, MatIconModule, MatFormFieldModule, MatInputModule, MatSortModule, MatOptionModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatTreeModule, MatCheckboxModule, MatDialogModule, MatError, MatProgressSpinnerModule } from '@angular/material';
import { UserService } from '../_services/user.service';
import { NuevousuarioComponent } from './seguridad/usuario/nuevousuario/nuevousuario.component';
import { EditarusuarioComponent } from './seguridad/usuario/editarusuario/editarusuario.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ListarolesComponent } from './seguridad/rol/listaroles/listaroles.component';
import { NuevorolComponent } from './seguridad/rol/nuevorol/nuevorol.component';
import { AsignaropcionesComponent } from './seguridad/rol/asignaropciones/asignaropciones.component';
import { TreeviewModule } from 'ngx-treeview';
import { AngularDualListBoxModule } from 'angular-dual-listbox';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';

import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { AngularDateTimePickerModule } from 'angular2-datetimepicker';

import { OrdenReciboService } from '../_services/Recepcion/ordenrecibo.service';
import { GeneralService } from '../_services/Mantenimiento/general.service';



import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { SpeedDialFabComponent } from './prerecibo/speed-dial-fab/speed-dial-fab.component';
import { Data } from '../_providers/data';
import { XHRBackend } from '@angular/http';
import { ApiXHRBackend } from '../_services/http.interceptor';

import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

import {MatProgressBarModule} from '@angular/material/progress-bar';
import { TwoDigitDecimaNumberDirective } from '../_common/TwoDigitDecimaNumberDirective';

import { DialogBuscarProducto } from './modal/ModalBuscarProducto/ModalBuscarProducto.component';

import { AgGridModule } from 'ag-grid-angular';
import { EditButtonRendererComponent } from './modal/Edit-button-renderer/Edit-button-renderer.component';


import { AngularSlickgridModule } from 'angular-slickgrid';
import { TranslateModule } from '@ngx-translate/core';

import { DialogNuevaFactura } from './modal/ModalNuevaFactura/ModalNuevaFactura.component';

import { DialogAsignarPuerta } from './modal/ModalAsignarPuerta/ModalAsignarPuerta.component';
import { DialogAsignarTrabajador } from './modal/ModalAsignarTrabajador/ModalAsignarTrabajador.component';




import { CellRendererProductos } from '../_common/Renderers/cellRendererProductos/cellRendererProductos.component';

import {DragDropModule} from 'primeng/dragdrop';
import {AutoCompleteModule} from 'primeng/autocomplete';

import {ButtonModule} from 'primeng/button';
import {DropdownModule} from 'primeng/dropdown';
import {TableModule} from 'primeng/table';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {ProgressBarModule} from 'primeng/progressbar';

import {CalendarModule} from 'primeng/calendar';

import { ListadoordentransporteComponent } from './seguimiento/ordentransporte/listadoordentransporte/listadoordentransporte.component';



@NgModule({
  declarations: [

    AccountSettingsComponent,
    ListausuariosComponent,
    NuevousuarioComponent,
    EditarusuarioComponent,
    ListarolesComponent,
    NuevorolComponent,
    AsignaropcionesComponent,
    NgbdModalConfirmAutofocus,
    DialogOverviewExampleDialog,
    DialogBuscarProducto,

    DialogNuevaFactura,
    SpeedDialFabComponent,
    TwoDigitDecimaNumberDirective,
    EditButtonRendererComponent,
    DialogAsignarPuerta,
    DialogAsignarTrabajador,
    CellRendererProductos,
    ListadoordentransporteComponent

  ],
  exports: [
   
      
  ],
  imports: [
    SharedModule,
    PAGES_ROUTES,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatOptionModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTreeModule,
    MatDialogModule,
    MatCheckboxModule,
    NgbModule,
    CalendarModule,
    TreeviewModule.forRoot(),
    AngularSlickgridModule.forRoot(),
    TranslateModule.forRoot(),
    NgxLoadingModule.forRoot({
      // animationType: ngxLoadingAnimationTypes.wanderingCubes,
      // backdropBackgroundColour: 'rgba(0,0,0,0.1)', 
      // backdropBorderRadius: '4px',
      // primaryColour: '#ffffff', 
      // secondaryColour: '#ffffff', 
      // tertiaryColour: '#ffffff'
  }),
    NgxMaterialTimepickerModule.forRoot(),
    AngularDualListBoxModule ,
    AngularDateTimePickerModule,
    NgxMatSelectSearchModule,
    MatSelectModule,
    SweetAlert2Module,
    MatProgressBarModule,
    AgGridModule.withComponents([
      CellRendererProductos
    ]),
    TableModule,
    ConfirmDialogModule,
    ProgressBarModule,
    MatProgressSpinnerModule,
    ButtonModule,
    DropdownModule,
    DragDropModule,
    AutoCompleteModule,
    
    
  ],
  providers: [
    AuthGuard,
    UserService,
    OrdenReciboService,
    GeneralService,
    Data,
    { provide: XHRBackend, useClass: ApiXHRBackend }
  ],
  entryComponents: [ 
    NgbdModalConfirmAutofocus,
    DialogOverviewExampleDialog, 
    DialogBuscarProducto,

    DialogNuevaFactura,
    EditButtonRendererComponent,
    DialogAsignarPuerta,
    DialogAsignarTrabajador
  ],
  
})

export class PagesModule {
}
