import { Directive, Input, ElementRef, Renderer2,OnChanges } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
  standalone: false
})
export class Highlight {

  @Input('appHighlight') price: number | undefined;

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  ngOnChanges() {
    const isPremium = (this.price ?? 0) > 2000;
    if (isPremium) {
      this.renderer.addClass(this.el.nativeElement, 'premium-highlight');
    } else {
      this.renderer.removeClass(this.el.nativeElement, 'premium-highlight');
    }
  }

}
