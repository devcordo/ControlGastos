import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";
import { Deposito } from "../models/deposito.model";
import { FondoMonetario } from "../models/fondo-monetario.model";

@Injectable({ providedIn: 'root' })
export class DepositosService {
    constructor(private api: ApiService) {}

    getAllDepositos(): Observable<Deposito[]> {
        return this.api.get<Deposito[]>('Depositos');
    }

    getDepositos(fechaInicio: string, fechaFin: string): Observable<Deposito[]> {
        return this.api.get<Deposito[]>('Depositos/Range', { fechaInicio, fechaFin });
    }

    crear(deposito: Deposito, fondo: FondoMonetario): Observable<Deposito> {
        return this.api.post<Deposito>(`Fondos/${fondo.id}/depositos`, deposito);
    }
}