import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'propertyValueGetter',
  standalone: false
})
export class PropertyValueGetterPipe implements PipeTransform {

  transform(value: any, propertyName: string): any {
    return value[propertyName];
  }

}
