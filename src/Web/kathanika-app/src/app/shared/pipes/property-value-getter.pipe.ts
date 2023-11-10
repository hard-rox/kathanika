import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'propertyValueGetter',
  standalone: false
})
export class PropertyValueGetterPipe implements PipeTransform {

  transform(value: never, propertyName: string): unknown {
    return value[propertyName];
  }

}
