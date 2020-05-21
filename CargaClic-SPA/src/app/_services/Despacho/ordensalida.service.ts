import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { OrdenRecibo } from 'src/app/_models/Recepcion/ordenrecibo';
import { environment } from 'src/environments/environment';
import { OrdenSalida } from 'src/app/_models/Despacho/ordenrecibo';
import { Carga } from 'src/app/_models/Despacho/Carga';
import { ShipmentLine, Shipment } from 'src/app/_models/Despacho/shipmentline';


const httpOptions = {
  headers: new HttpHeaders({
    'Authorization' : 'Bearer ' + localStorage.getItem('token'),
    'Content-Type' : 'application/json'
  }),
}
  

@Injectable({
  providedIn: 'root'
})
export class OrdenSalidaService {
  baseUrl = environment.baseUrl + '/api/ordensalida/';
constructor(private http: HttpClient) { }

registrar_salidacarga(model:any) { 
  return this.http.post(this.baseUrl + 'RegisterSalidaShipment', model,httpOptions);
}
RegistarOrdenSalida(model: any){
  return this.http.post(this.baseUrl + 'RegisterOrdenSalida', model,httpOptions);
}
ActualizarOrdenSalida(model: any){
  return this.http.post(this.baseUrl + 'UpdateOrdenSalida', model,httpOptions);
}

PlanificarPicking(model: any){
  return this.http.post(this.baseUrl + 'PlanificarPicking', model,httpOptions);
}



getAllOrdenSalida(model: any) : Observable<OrdenSalida[]> {
  //console.log(this.model);

  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.estadoIdfiltro +
  "&fec_ini=" + model.fec_ini.toLocaleDateString() +
  "&fec_fin=" + model.fec_fin.toLocaleDateString() +
  "&AlmacenId=" + model.AlmacenId ;
  
  return this.http.get<OrdenSalida[]>(this.baseUrl + "GetAllOrder" + params,httpOptions)
};
getAllPendienteCarga(model: any) : Observable<ShipmentLine[]> {
  let params = "";
  return this.http.get<ShipmentLine[]>(this.baseUrl + "GetAllPendienteCarga" + params,httpOptions)
};

getAllPickingPendiente() : Observable<Shipment[]> {
  let params = "";
  return this.http.get<Shipment[]>(this.baseUrl + "ListarPickingPendiente" + params,httpOptions)
};

getAllPickingPendienteDetalle(Id) : Observable<ShipmentLine[]> {
  let params = "?ShipmentId=" + Id;
  return this.http.get<ShipmentLine[]>(this.baseUrl + "ListarPickingPendienteDetalle" + params,httpOptions)
};



getAllOrdenSalidaPendientes(model: any) : Observable<OrdenSalida[]> {

  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.EstadoId + 
  "&DaysAgo=" + model.intervalo +
  "&AlmacenId=" + model.AlmacenId;

  return this.http.get<OrdenSalida[]>(this.baseUrl + "GetAllOrderPendiente" + params,httpOptions)
};


getAllCargas(model: any) : Observable<Carga[]> {
  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.EstadoId ;
  return this.http.get<Carga[]>(this.baseUrl + "GetAllCargas" + params,httpOptions)
};

getAllCargas_pendientes(model: any) : Observable<Carga[]> {
  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.EstadoId ;
  return this.http.get<Carga[]>(this.baseUrl + "GetAllCargas_Pendientes_Salida" + params,httpOptions)
};
getAllWork(model: any) : Observable<Carga[]> {
  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.EstadoId ;
  return this.http.get<Carga[]>(this.baseUrl + "GetAllWork" + params,httpOptions)
};

getAllWork_Asignado(model: any) : Observable<Carga[]> {
  let params = "?PropietarioID=" + model.PropietarioId +
  "&EstadoId=" + model.EstadoId ;
  return this.http.get<Carga[]>(this.baseUrl + "GetAllWorkAssigned" + params,httpOptions)
};

getAllWorkDetail(id: number) : Observable<Carga[]> {
  let params = "?WrkId=" + id ;
  return this.http.get<Carga[]>(this.baseUrl + "getAllWorkDetail" + params,httpOptions)
};

getAllByEquipoTransporte(model: any) : Observable<OrdenRecibo[]> {
  let params = "?EquipoTransporteId=" + model.EquipoTransporteId ;
  return this.http.get<OrdenRecibo[]>(this.baseUrl + "GetOrderbyEquipoTransporte" + params,httpOptions)
};

actualizar(model: any){
  return this.http.post(this.baseUrl + 'update', model,httpOptions);
}

obtenerOrden(id: any): Observable<OrdenSalida> {
  return this.http.get<OrdenSalida>(this.baseUrl +"GetOrder?OrdenSalidaId=" + id, httpOptions);
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
  return this.http.post(this.baseUrl + 'MatchTransporteCarga', model,httpOptions);
}

registrar_carga(model: any){
  return this.http.post(this.baseUrl + 'RegisterCarga', model,httpOptions)
  .pipe(
    map((response: any) => {
    } 
   )
)};



deleteOrder(id:any) : Observable<OrdenSalida[]> {
  let params = "?OrdenSalidaId=" + id ;
  return this.http.delete<OrdenSalida[]>(this.baseUrl + 'DeleteOrder' + params,httpOptions);
}
deleteOrderDetail(id:any) : Observable<OrdenSalida[]> {
  let params = "?id=" + id ;
  return this.http.delete<OrdenSalida[]>(this.baseUrl + 'DeleteOrderDetail' + params,httpOptions);
}



assignmentOfDoor(ids: string , ubicacionId: number) {
    let model: any = {};
    model.ids = ids;
    model.PuertaId = ubicacionId;

    return this.http.post(this.baseUrl + 'assignmentOfDoor', model,httpOptions)
    .pipe(
      map((response: any) => {
      } 
    )
)}
assignmentOfUser(ids: string , UserId: number) {
  let model: any = {};
  model.ids = ids;
  model.UserId = UserId;

  return this.http.post(this.baseUrl + 'assignmentOfUser', model,httpOptions)
  .pipe(
    map((response: any) => {
    } 
  )
)}
movimientoSalida(Id:number){
  //RegisterInventario
  let model: any = {};
  model.Id = Id;

  return this.http.post(this.baseUrl + 'MovimientoSalida', model,httpOptions)
  .pipe(
    map((response: any) => {
    } 
  )
)};
// obtenerOrdenDetalle(id: any): Observable<OrdenReciboDetalle> {
//     return this.http.get<OrdenReciboDetalle>(this.baseUrl +"GetOrderDetail?Id=" + id, httpOptions);
//    }

// identificar_detalle(model: any){
//   return this.http.post(this.baseUrl + 'identify_detail', model,httpOptions)
//   .pipe(
//     map((response: any) => {
//     } 
//    )
// )};
// cerrar_identificacion(Id: any){
  
//   let model: any;
//   return this.http.post(this.baseUrl + 'close_details?Id='+ Id,model,httpOptions)
//   .pipe(
//     map((response: any) => {
//     } 
//    )
// )};


}