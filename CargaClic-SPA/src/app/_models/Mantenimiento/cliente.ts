export interface Cliente {
    idcliente: number;
    nombre: string;
    razonsocial: string;
    tipoDocumentoId: number;
    documento: string;
}
export interface Ubigeo {
    iddistrito: number;
    idprovincia: number;
    iddepartamento: number;
    ubigeo: string;
    
}
export interface OrdenTransporte {
        idordentrabajo  : number;
        numcp  : string;
        razonsocial  : string;
        // idtipotransporte  : number;
        // idconceptocobro  : number;
        destino  : string;
        remitente  : string;
        destinatario  : string;
        tipotransporte  : string;
        conceptocobro  : string;
        //idpreliquidacion  : number;
        fecharegistro  : Date;
        fechadespacho  : Date;
        fechaentrega  : Date;
        fecharecojo  : Date;
        peso  : number;
        volumen  : number;
        bulto : number;
        docgeneral  : string;
        GRR  : string;
        estado  : string;
}




