import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { OrdenRecibo, OrdenReciboDetalle } from 'src/app/_models/Recepcion/ordenrecibo';
import { Ubicacion } from 'src/app/_models/Mantenimiento/ubicacion';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { EquipoTransporte } from 'src/app/_models/Recepcion/equipotransporte';
import { environment } from 'src/environments/environment';
import { InventarioGeneral } from 'src/app/_models/Inventario/inventariogeneral';
import { Cliente, Ubigeo, OrdenTransporte } from 'src/app/_models/Mantenimiento/cliente';


const httpOptions = {
  headers: new HttpHeaders({
    'Authorization' : 'Bearer ' + localStorage.getItem('token'),
    'Content-Type' : 'application/json'
  }),
}
  

@Injectable({
  providedIn: 'root'
})
export class OrdenReciboService {
  baseUrl = environment.baseUrl + '/api/seguimiento/';
constructor(private http: HttpClient) { }

getClientes(criterio) : Observable<Cliente[]> {
  return this.http.get<Cliente[]>(this.baseUrl + "GetAllClients?idscliente="+ criterio   ,httpOptions)
} 
getUbigeo(criterio) : Observable<Ubigeo[]> {
  return this.http.get<Ubigeo[]>(this.baseUrl + "GetListUbigeo?criterio="+ criterio  ,httpOptions)
} 
getAllOrderTransport(model: any) {
    if(model.idestado === 0)
     model.idestado = "";

     if(model.idcliente=== 0)
        model.idcliente = ""

     if(model.iddistrito === 0)
     model.iddistrito = "";

  var param = "?idcliente=" + model.idcliente + "&numcp=" + model.numcp 
  + "&fecinicio=" + model.fec_ini.toLocaleDateString()
  + "&fecfin=" + model.fec_fin.toLocaleDateString()
  + "&grr=" + model.grr  
  + "&docreferencia=" + model.docreferencia 
  + "&idestado=" + model.idestado 
  + "&iddestino=" + model.iddistrito 
  + "&idusuario=" + model.idusuario ;

  console.log(param);
  
 return this.http.get<OrdenTransporte[]>(this.baseUrl + "GetAllOrder" + param  ,httpOptions)
 }


getAll(model: any) : Observable<OrdenRecibo[]> {

  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.EstadoId +
  "&fec_ini=" + model.fec_ini.toLocaleDateString() +
  "&fec_fin=" + model.fec_fin.toLocaleDateString() +
  "&AlmacenId=" + model.AlmacenId;

  return this.http.get<OrdenRecibo[]>(this.baseUrl + params,httpOptions)
};
getAllByEquipoTransporte(model: any) : Observable<OrdenRecibo[]> {
  let params = "?EquipoTransporteId=" + model.EquipoTransporteId ;
  return this.http.get<OrdenRecibo[]>(this.baseUrl + "GetOrderbyEquipoTransporte" + params,httpOptions)
};


registrar(model: any){
  return this.http.post(this.baseUrl + 'register', model,httpOptions);
}

actualizar(model: any){
  return this.http.post(this.baseUrl + 'update', model,httpOptions);
}

obtenerOrden(id: any): Observable<OrdenRecibo> {
  return this.http.get<OrdenRecibo>(this.baseUrl +"GetOrder?Id=" + id, httpOptions);
}

registrar_detalle(model: any){
  return this.http.post(this.baseUrl + 'register_detail', model,httpOptions)
  .pipe(
    map((response: any) => {
    } 
   )
)};

vincularEquipoTransporte(model: any){
    return this.http.post(this.baseUrl + 'RegisterEquipoTransporte', model,httpOptions);
}
matchEquipoTransporte(model: any){
  return this.http.post(this.baseUrl + 'MatchTransporteOrdenIngreso', model,httpOptions);
}
getEquipoTransporte(placa: string) : Observable<EquipoTransporte> {
  
  return this.http.get<EquipoTransporte>(this.baseUrl +"GetEquipoTransporte?placa=" + placa ,httpOptions)
};

getAllEquipoTransporte(model:any) : Observable<EquipoTransporte[]> {

  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.EstadoId +
  "&fec_ini=" + model.fec_ini.toLocaleDateString() +
  "&fec_fin=" + model.fec_fin.toLocaleDateString() +
  "&AlmacenId=" + model.AlmacenId;

  return this.http.get<EquipoTransporte[]>(this.baseUrl + 'ListEquipoTransporte' + params ,httpOptions);
}
deleteOrder(id:any) : Observable<OrdenRecibo[]> {
  let params = "?OrdenReciboId=" + id ;
  return this.http.delete<OrdenRecibo[]>(this.baseUrl + 'DeleteOrder' + params,httpOptions);
}
deleteOrderDetail(id:any) : Observable<OrdenRecibo[]> {
  let params = "?id=" + id ;
  return this.http.delete<OrdenRecibo[]>(this.baseUrl + 'DeleteOrderDetail' + params,httpOptions);
}



assignmentOfDoor(EquipoTransporteId: any , ubicacionId: number) {
    let model: any = {};
    model.EquipoTransporteId = EquipoTransporteId;
    model.ubicacionId = ubicacionId;

    return this.http.post(this.baseUrl + 'assignmentOfDoor', model,httpOptions)
    .pipe(
      map((response: any) => {
      } 
    )
)}

obtenerOrdenDetalle(id: any): Observable<OrdenReciboDetalle> {
    return this.http.get<OrdenReciboDetalle>(this.baseUrl +"GetOrderDetail?Id=" + id, httpOptions);
   }

identificar_detalle(model: any){
   console.log(model);
  return this.http.post(this.baseUrl + 'identify_detail', model,httpOptions)
  .pipe(
    map((response: any) => {
    } 
   )
)};

identificar_detallemultiple(model: InventarioGeneral[]){
  let body = JSON.stringify(model);  
  return this.http.post(this.baseUrl + 'identify_detail_mix', body,httpOptions)
  .pipe(
    map((response: any) => {
      
    } 
   )
)};

cerrar_identificacion(Id: any){
  
  let model: any;
  return this.http.post(this.baseUrl + 'close_details?Id='+ Id,model,httpOptions)
  .pipe(
    map((response: any) => {
    } 
   )
)};


}