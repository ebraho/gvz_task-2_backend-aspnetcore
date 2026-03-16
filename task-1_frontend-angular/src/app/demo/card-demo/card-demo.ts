import { Component } from '@angular/core';

import { PartDemo } from '../part-demo';
import { Card } from '../../parts/card/card';

@Component({
  selector: 'app-card-demo',
  imports: [Card, PartDemo],
  templateUrl: './card-demo.html',
  styleUrls: ['./card-demo.scss', '../part-demo-common.scss'],
})
export class CardDemo {
  protected label = 'Karte';
  protected description =
    'Darstellung von Information oder Gruppierung von Eingaben.';
  protected parameters = [
    {
      name: 'Aussehen',
      valueRange: 'Gefüllt oder Umrandet',
      required: false,
      defaultValue: 'Gefüllt',
    },
  ];
}
