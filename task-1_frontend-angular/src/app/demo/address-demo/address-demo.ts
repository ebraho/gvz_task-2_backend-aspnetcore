import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TextInput } from '../../parts/text-input/text-input';
import { NumberInput } from '../../parts/number-input/number-input';
import { Select } from '../../parts/select/select';
import { PartDemo } from '../part-demo';
import { Card } from '../../parts/card/card';
import { SWISS_CANTONS } from './cantons';

@Component({
  selector: 'app-address-demo',
  imports: [ReactiveFormsModule, Card, TextInput, NumberInput, PartDemo, Select],
  templateUrl: './address-demo.html',
  styleUrls: ['./address-demo.scss', '../part-demo-common.scss'],
})
export class AddressDemo {
  protected label = 'Postadresse';
  protected description = [
    'Dieses Beispiel zeigt eine Kombination mehrerer Bauteile zur Eingabe einer Postadresse.',
  ];
  protected readonly cantons = SWISS_CANTONS;

  protected addressForm = new FormGroup({
    street: new FormControl<string>('', [Validators.required, Validators.maxLength(100)]),
    houseNumber: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(1),
      Validators.max(99999),
    ]),
    zipCode: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(1000),
      Validators.max(9999),
    ]),
    city: new FormControl<string>('', [Validators.required, Validators.maxLength(100)]),
    canton: new FormControl<number | null>(null, [Validators.required]),
  });

  protected get streetControl() {
    return this.addressForm.controls.street;
  }

  protected get houseNumberControl() {
    return this.addressForm.controls.houseNumber;
  }

  protected get zipCodeControl() {
    return this.addressForm.controls.zipCode;
  }

  protected get cityControl() {
    return this.addressForm.controls.city;
  }

  protected get cantonControl() {
    return this.addressForm.controls.canton;
  }
}
