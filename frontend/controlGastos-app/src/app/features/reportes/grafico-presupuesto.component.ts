
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistroGastosService } from '../../core/services/registro-gastos.service';
import { PresupuestoService } from '../../core/services/presupuesto.service';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Chart } from 'chart.js/auto';
import { TipoGasto } from '../../core/models/tipo-gasto.model';
import { TiposGastoService } from '../../core/services/tipos-gasto.service';

@Component({
  selector: 'app-grafico-presupuesto',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule],
  templateUrl: './grafico-presupuesto.component.html'
})
export class GraficoPresupuestoComponent {
  fechaInicio: Date = new Date();
  fechaFin: Date = new Date();
  datosGrafico: any[] = [];
  tipoGastoObj: TipoGasto[] = [];
  presupuestoEjecucionChart: any;
  presupuestoChart: any;

  constructor(
    private gastosService: RegistroGastosService, 
    private presupuestoService: PresupuestoService,
    private tiposService: TiposGastoService)
    {
        this.loadTipos();
    }

  loadTipos(){
    this.tiposService.getTiposGasto().subscribe(data => this.tipoGastoObj = data);
  }

  cargarGrafico() {
    this.datosGrafico = [];
    
    const mes = this.fechaInicio.getMonth() + 1;
    const anio = this.fechaInicio.getFullYear();

    this.presupuestoService.getPresupuestos(mes, anio).subscribe(presupuestos => {
      this.gastosService.getGastosConsulta(
        this.fechaInicio.toISOString(),
        this.fechaFin.toISOString()
      ).subscribe(gastos => {
        this.datosGrafico = presupuestos.map(p => {
          const ejecutado = gastos
            .filter(g => g.tipo === "Gasto") // Solo gastos
            .filter(g => Number(g.tipoGastoId) === p.tipoGastoId) // Tipo de gasto correcto
            .reduce((sum, g) => sum + g.monto, 0); // Sumar montos

            return {
                tipoGasto: p.tipoGastoId,
                presupuesto: p.monto,
                ejecutado
            };
        });

        this.dibujarGrafico();
      });
    });
  }

  dibujarGrafico() {
    const pex = document.getElementById('graficoPresupuestoEjecucion') as HTMLCanvasElement;
    const ctx = document.getElementById('graficoPresupuesto') as HTMLCanvasElement;


    if (this.presupuestoEjecucionChart) {
        console.log("Destruyendo chart viejo...");
        this.presupuestoEjecucionChart.destroy();
    }

    this.presupuestoEjecucionChart = new Chart(pex, {
      type: 'bar',
      data: {
        labels: this.datosGrafico.map(x => this.tipoGastoObj.find(t => t.id === x.tipoGasto)?.nombre),
        datasets: [
          {
            label: 'Presupuesto',
            data: this.datosGrafico.map(x => x.presupuesto)
          },
          {
            label: 'Ejecutado',
            data: this.datosGrafico.map(x => x.ejecutado)
          }
        ]
      },
      options: {
        plugins: {
            title: {
                display: true,
                text: "Presupuesto vs Ejecutado"
            }
        }
      }
    });

    if (this.presupuestoChart) {
        console.log("Destruyendo chart viejo...");
        this.presupuestoChart.destroy();
    }

    this.presupuestoChart = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: this.datosGrafico.map(x => this.tipoGastoObj.find(t => t.id === x.tipoGasto)?.nombre),
        datasets: [
          {
            label: 'Presupuesto',
            data: this.datosGrafico.map(x => x.presupuesto)
          }
        ]
      },
      options: {
        plugins: {
            title: {
                display: true,
                text: "Presupuesto mes"
            }
        }
      }
    });
  }
}
