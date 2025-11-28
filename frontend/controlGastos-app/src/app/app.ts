import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule, MatToolbarModule, MatButtonModule, MatMenuModule],
  templateUrl: './app.html',
})
export class App {
  constructor(private router: Router) {}

  buttonOptions = {
    mantenimientos: { text: 'Mantenimientos' },
    movimientosMenu: { text: 'Movimientos' },
    reportes: { text: 'Consultas y Reportes' },
    tiposGasto: { text: 'Tipos de Gasto', onClick: () => this.router.navigate(['/tipos-gasto']) },
    fondoMonetario: { text: 'Fondo Monetario', onClick: () => this.router.navigate(['/fondo-monetario']) },
    presupuesto: { text: 'Presupuesto', onClick: () => this.router.navigate(['/presupuesto']) },
    registroGastos: { text: 'Registro Gastos', onClick: () => this.router.navigate(['/registro-gastos']) },
    depositos: { text: 'Depósitos', onClick: () => this.router.navigate(['/depositos']) },
    movimientos: { text: 'Movimientos', onClick: () => this.router.navigate(['/consulta-movimientos']) },
    grafico: { text: 'Gráfico', onClick: () => this.router.navigate(['/grafico-presupuesto']) }
  };
}
