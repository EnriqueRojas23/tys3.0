import { RouterModule, Routes } from '@angular/router';



import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { ListausuariosComponent } from './seguridad/usuario/listausuarios/listausuarios.component';
import { NuevousuarioComponent } from './seguridad/usuario/nuevousuario/nuevousuario.component';
import { EditarusuarioComponent } from './seguridad/usuario/editarusuario/editarusuario.component';
import { ListarolesComponent } from './seguridad/rol/listaroles/listaroles.component';
import { NuevorolComponent } from './seguridad/rol/nuevorol/nuevorol.component';
import { AsignaropcionesComponent } from './seguridad/rol/asignaropciones/asignaropciones.component';
import { AuthGuard } from '../_guards/auth.guard';








import { ListadoordentransporteComponent } from './seguimiento/ordentransporte/listadoordentransporte/listadoordentransporte.component';



const pagesRoutes: Routes = [


    
    {path : 'account-settings', component : AccountSettingsComponent, canActivate: [AuthGuard]} ,

    {path : 'seguridad/listaroles', component : ListarolesComponent , canActivate: [AuthGuard]} ,
    {path : 'seguridad/nuevorol', component : NuevorolComponent , canActivate: [AuthGuard]} ,
    {path : 'seguridad/asignaropciones/:uid', component : AsignaropcionesComponent} ,
    {path : 'seguridad/listausuarios', component : ListausuariosComponent, canActivate: [AuthGuard]} ,
    {path : 'seguridad/nuevousuario', component : NuevousuarioComponent, canActivate: [AuthGuard]} ,
    {path : 'seguridad/editarusuario/:uid', component : EditarusuarioComponent, canActivate: [AuthGuard]} ,

    
    {path : 'seguimiento/listadoordentransporte', component : ListadoordentransporteComponent, canActivate: [AuthGuard]} ,
    
 




    {path : '', redirectTo : '/login', pathMatch: 'full'},

];

export const PAGES_ROUTES = RouterModule.forChild( pagesRoutes );

