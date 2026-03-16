import { Component, computed, input, Signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-part-demo',
  imports: [MatTableModule],
  templateUrl: './part-demo.html',
  styleUrl: './part-demo.scss',
})
export class PartDemo {
  label = input.required<string>();
  description = input.required<string | string[]>();
  parameters = input<
    {
      name: string;
      valueRange: string;
      required: boolean;
      defaultValue?: string;
    }[]
  >([]);

  protected columns: string[] = [
    'name',
    'valueRange',
    'required',
    'defaultValue',
  ];
  protected descriptionIsArray: Signal<boolean> = computed(() =>
    Array.isArray(this.description()),
  );
}
