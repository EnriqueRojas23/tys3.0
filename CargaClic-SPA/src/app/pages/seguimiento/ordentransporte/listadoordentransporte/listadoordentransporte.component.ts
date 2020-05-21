import { Component, OnInit } from '@angular/core';
import { OrdenReciboService } from 'src/app/_services/Recepcion/ordenrecibo.service';
import { SelectItem } from 'primeng/components/common/selectitem';
import { OrdenTransporte } from 'src/app/_models/Mantenimiento/cliente';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { AppDateAdapter, APP_DATE_FORMATS } from 'src/app/pages/account-settings/datepicker.extend';
import * as moment from 'moment';
import * as XLSX from 'xlsx';
import * as FileSaver from 'file-saver';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-listadoordentransporte',
  templateUrl: './listadoordentransporte.component.html',
  styleUrls: ['./listadoordentransporte.component.css'],
  providers: [
    {
        provide: DateAdapter, useClass: AppDateAdapter
    },
    {
        provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
    }
    ]
})
export class ListadoordentransporteComponent implements OnInit {
  clientes: SelectItem[] = [];
  ubigeo: SelectItem[] = [];
  estados: SelectItem[] = [];

  ordenes: OrdenTransporte[] = [];

  model: any = {};
  ProveedorLoaded : boolean = false;
  UbigeoLoaded : boolean = false;
  cols: any[];  
  es: any;
  frozenCols: any[];
  user: User ;

  dateInicio: Date = new Date(Date.now()) ;
  dateFin: Date = new Date(Date.now()) ;

  constructor(private ordenreciboService: OrdenReciboService,) { }

  ngOnInit() {

    this.user = JSON.parse(localStorage.getItem("user"));
    
    this.model.idusuario = this.user.usr_int_id;
    this.dateInicio.setDate((new Date()).getDate() - 5);
    this.dateFin.setDate((new Date()).getDate() );
    this.model.numcp = "";
    this.model.docreferencia = "";
    this.model.grr = "";



    this.cols = 
    [
         {header: 'OT', field: 'numcp'  ,  width: '120px' },
        {header: 'F. REGISTRO', field: 'fecharegistro_string' , width: '140px'  },
        {header: 'RAZÓN SOCIAL', field: 'razonsocial' , width: '260px' },
        {header: 'DOC REF', field: 'docgeneral' , width: '120px'  },
        {header: 'TIP TRANS', field: 'fecha_carga' , width: '120px'  },
        {header: 'ESTADO', field: 'estado'  , width: '140px'   },
        {header: 'F. RECOJO', field: 'fecharecojo' , width: '140px'  },
        {header: 'F. DESPACHO', field: 'fechadespacho' , width: '140px'  },
        {header: 'F. ENTREGA', field: 'fechaentrega' , width: '140px'  },
        {header: 'DESTINO', field: 'destino'  ,  width: '180px'  },
        {header: 'REMITENTE', field: 'remitente'  ,  width: '180px'  },
        {header: 'DESTINATARIO', field: 'destinatario' , width: '180px'  },


  
      ];
    


    this.es = {
      firstDayOfWeek: 1,
      dayNames: [ "domingo","lunes","martes","miércoles","jueves","viernes","sábado" ],
      dayNamesShort: [ "dom","lun","mar","mié","jue","vie","sáb" ],
      dayNamesMin: [ "D","L","M","X","J","V","S" ],
      monthNames: [ "enero","febrero","marzo","abril","mayo","junio","julio","agosto","septiembre","octubre","noviembre","diciembre" ],
      monthNamesShort: [ "ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic" ],
      today: 'Hoy',
      clear: 'Borrar'
  }
   
    this.ordenreciboService.getClientes(this.user.idclientes).subscribe(resp => {

        this.clientes.push({ value: 0,  label : "TODOS LOS CLIENTES"});

          resp.forEach(element => {
            this.clientes.push({ value: element.idcliente ,  label : element.razonsocial});
          });

         this.ProveedorLoaded = true;
         this.model.idcliente =0;

      });

      this.ordenreciboService.getUbigeo("").subscribe(resp => {

        this.ubigeo.push({ value: 0,  label : "TODOS LOS DESTINOS"});

          resp.forEach(element => {
            this.ubigeo.push({ value: element.iddistrito ,  label : element.ubigeo});
          });

         this.UbigeoLoaded = true;
         this.model.iddistrito = 0;

      });
      this.estados.push({ value: 0,  label : "TODOS LOS ESTADOS"});
      this.estados.push({ value: 1,  label : "Por Despachar"});
      this.estados.push({ value: 2,  label : "Por Entregar"});
      this.estados.push({ value: 3,  label : "Entregado"});
      this.model.idestado = 0;



  }
  buscar(){
   

      this.model.fec_ini = this.dateInicio;
      this.model.fec_fin = this.dateFin;

      console.log(this.model);
  
    this.ordenreciboService.getAllOrderTransport(this.model).subscribe(list => {
    
        this.ordenes =  list;

     
    });
  }

 exportExcel() {
    import("xlsx").then(xlsx => {
        const worksheet = xlsx.utils.json_to_sheet( this.ordenes );
        const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
        const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
        this.saveAsExcelFile(excelBuffer, "ListaOT");
    });
}
saveAsExcelFile(buffer: any, fileName: string): void {
  import("file-saver").then(FileSaver => {
      let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
      let EXCEL_EXTENSION = '.xlsx';
      const data: Blob = new Blob([buffer], {
          type: EXCEL_TYPE
      });
      FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
  });
}
}
