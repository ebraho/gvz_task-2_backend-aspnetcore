import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { PartDemo } from '../part-demo';
import { NumberInput } from '../../parts/number-input/number-input';

@Component({
  selector: 'app-number-input-demo',
  imports: [NumberInput, PartDemo],
  templateUrl: './number-input-demo.html',
  styleUrls: ['./number-input-demo.scss', '../part-demo-common.scss'],
})
export class NumberInputDemo {
  protected label = 'Zahleingabe';
  protected description = [
    'Eingabe von Zahlen.',
    'Ein minimaler und maximaler Wert kann festgelegt werden.',
  ];
  protected parameters = [
    { name: 'Label', valueRange: '<Text>', required: true },
    { name: 'Minimum', valueRange: '<Zahl>', required: false },
    { name: 'Maximum', valueRange: '<Zahl>', required: false },
  ];

  protected numberFormControl = new FormControl<number | null>(null);
  protected numberConstrainedFormControl = new FormControl<number | null>(
    null,
    [Validators.required, Validators.min(1000), Validators.max(9999)],
  );
}
