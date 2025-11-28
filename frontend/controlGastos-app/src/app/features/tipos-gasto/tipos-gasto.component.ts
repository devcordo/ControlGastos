import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TiposGastoService } from '../../core/services/tipos-gasto.service';
import { TipoGasto } from '../../core/models/tipo-gasto.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-tipos-gasto',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    ReactiveFormsModule,  
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule],
  templateUrl: './tipos-gasto.component.html'
})
export class TiposGastoComponent {
    tipoGastoForm: FormGroup;
    tiposGasto: any[] = [];
    editingTipo: TipoGasto = { id: 0, codigo: '', nombre: '', descripcion: '' };

    displayedColumns = ['nombre', 'descripcion', 'acciones'];

    constructor(private fb: FormBuilder ,private service: TiposGastoService) {
        this.tipoGastoForm = this.fb.group({
            nombre: ['', Validators.required],
            descripcion: [''],
        });
        this.loadTipos();
    }

    loadTipos() {
        this.service.getTiposGasto().subscribe(data => this.tiposGasto = data);
    }

    guardar() {
        if (this.editingTipo.id) {
            this.service.actualizar(this.editingTipo).subscribe(() => this.loadTipos());
        } else {
            this.service.crear(this.editingTipo).subscribe(() => this.loadTipos());
            this.tipoGastoForm.reset({nombre: null, descripcion: null});
        }
    }

    eliminar(id: number) {
        if (id != null) {
            this.service.eliminar(id).subscribe(() => this.loadTipos());
        }
    }
}