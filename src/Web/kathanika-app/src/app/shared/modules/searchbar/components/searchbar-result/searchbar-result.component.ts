import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { SearchbarComponent } from '../searchbar/searchbar.component';

@Component({
  selector: 'kn-searchbar-result',
  templateUrl: './searchbar-result.component.html',
  styleUrls: ['./searchbar-result.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SearchbarResultComponent<T> {
  @Input({ required: true }) value!: T;

  constructor(private parent: SearchbarComponent<T>) { }

  protected onOptionSelect() {
    this.parent.resultSelected.emit(this.value);
  }
}
