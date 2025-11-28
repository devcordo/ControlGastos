import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";
import { Presupuesto } from "../models/presupuesto.model";

@Injectable({ providedIn: 'root' })
export class PresupuestoService {
    constructor(private api: ApiService) {}

    getAllPresupuestos(): Observable<Presupuesto[]> {
        return this.api.get<Presupuesto[]>('Presupuestos');
    }

    getPresupuestos(mes: number, anio: number): Observable<Presupuesto[]> {
        return this.api.get<Presupuesto[]>('Presupuestos/Mes', { mes, anio });
    }

    crear(presupuesto: Presupuesto): Observable<Presupuesto> {
        return this.api.post<Presupuesto>('Presupuestos', presupuesto);
    }

    actualizar(presupuesto: Presupuesto): Observable<Presupuesto> {
        return this.api.put<Presupuesto>(`Presupuestos/${presupuesto.id}`, presupuesto);
    }

    eliminar(id: number): Observable<void> {
        return this.api.delete<void>(`Presupuestos/${id}`);
    }
}