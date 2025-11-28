import { Routes } from '@angular/router';
import { TiposGastoComponent } from './features/tipos-gasto/tipos-gasto.component';
import { FondoMonetarioComponent } from './features/fondo-monetario/fondo-monetario.component';
import { PresupuestoComponent } from './features/presupuesto/presupuesto.component';
import { RegistroGastosComponent } from './features/registro-gastos/registro-gastos.component';
import { DepositosComponent } from './features/depositos/depositos.component';
import { ConsultaMovimientosComponent } from './features/reportes/consulta-movimientos.component';
import { GraficoPresupuestoComponent } from './features/reportes/grafico-presupuesto.component';

export const routes: Routes = [
  { path: '', redirectTo: 'tipos-gasto', pathMatch: 'full' },
  { path: 'tipos-gasto', component: TiposGastoComponent },
  { path: 'fondo-monetario', component: FondoMonetarioComponent },
  { path: 'presupuesto', component: PresupuestoComponent },
  { path: 'registro-gastos', component: RegistroGastosComponent },
  { path: 'depositos', component: DepositosComponent },
  { path: 'consulta-movimientos', component: ConsultaMovimientosComponent },
  { path: 'grafico-presupuesto', component: GraficoPresupuestoComponent }
];
