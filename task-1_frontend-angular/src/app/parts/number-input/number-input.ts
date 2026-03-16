import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-number-input',
  imports: [MatInputModule, ReactiveFormsModule],
  templateUrl: './number-input.html',
  styleUrl: './number-input.scss',
})
export class NumberInput {
  control = input.required<FormControl<number | null>>(); // must not be named formControl to avoid conflict with Angular directive
  label = input.required<string>();
  max = input<number | null>(null);
  min = input<number | null>(null);

  get validRangeString(): string {
    if (this.min() !== null && this.max() !== null) {
      return `≥ ${this.min()}, ≤ ${this.max()}`;
    }

    if (this.min() !== null) {
      return `≥ ${this.min()}`;
    }

    return `≤ ${this.max()}`;
  }
}
