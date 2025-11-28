export interface GastoDetalle {
  tipoGastoId: number;
  monto: number;
}

export interface Gasto {
  id: number;
  tipo: string;
  fecha: Date;
  fondoMonetarioId: number;
  observaciones: string;
  nombreComercio: string;
  tipoDocumento: string;
  detalles: GastoDetalle[];
}

export interface GastoConsulta {
  id: number;
  tipo: string;
  fecha: Date;
  fondoMonetarioId: number;
  observaciones: string;
  nombreComercio: string;
  tipoDocumento: string;
  tipoGastoId: number;
  monto: number;
}