import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";
import { Gasto, GastoConsulta } from "../models/gasto.model";

@Injectable({ providedIn: 'root' })
export class RegistroGastosService {
    constructor(private api: ApiService) {}

    getGastos(desde: string, hasta: string): Observable<Gasto[]> {
        return this.api.get<Gasto[]>('Gastos/Movimientos', { desde, hasta });
    }

    getGastosConsulta(desde: string, hasta: string): Observable<GastoConsulta[]> {
        return this.api.get<GastoConsulta[]>('Gastos/Movimientos', { desde, hasta });
    }

    crear(gasto: Gasto): Observable<Gasto> {
        return this.api.post<Gasto>('Gastos', gasto);
    }
}