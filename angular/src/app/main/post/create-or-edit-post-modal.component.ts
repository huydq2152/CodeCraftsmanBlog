import { Component, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { SlugHelper } from '@app/shared/common/helper/helper';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    CCBServiceProxy,
    CreateOrEditPostDto,
    PostCategoryDto,
    PostDto,
    PostServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

@Component({
    selector: 'createOrEditPostModal',
    templateUrl: './create-or-edit-post-modal.component.html',
})
export class CreateOrEditPostModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    post: CreateOrEditPostDto = new CreateOrEditPostDto();

    filteredPostCategories: PostCategoryDto[];
    selectedPostCategory: PostCategoryDto;

    constructor(injector: Injector, private _postService: PostServiceProxy, private _ccbAppSerivce: CCBServiceProxy) {
        super(injector);
    }

    show(post?: CreateOrEditPostDto): void {
        this.active = true;
        if (post === undefined || post.id === undefined || post.id <= 0) {
            this.post = new CreateOrEditPostDto();
            this.modal.show();
        } else {
            this._postService.getPostForEdit(post.id).subscribe((postResult) => {
                this.post = postResult.post;
                this.selectedPostCategory = new PostCategoryDto();
                this.selectedPostCategory.id = this.post.postCategoryId;
                this.selectedPostCategory.code = this.post.postCategoryCode;
                this.selectedPostCategory.name = this.post.postCategoryName;
                this.modal.show();
            });
        }
    }

    onShown(): void {
        document.getElementById('Code').focus();
    }

    save(): void {
        let input = this.post;
        if (this.selectedPostCategory != undefined) {
            input.postCategoryId = this.selectedPostCategory.id;
        }

        this.saving = true;
        this._postService
            .createOrEdit(input)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    filterPostCategories(event): void {
        this._ccbAppSerivce.getPostCategories(event.query, undefined, undefined).subscribe((postCategories) => {
            this.filteredPostCategories = postCategories;
        });
    }

    public selectedItemDisplay(postCategory: PostCategoryDto): string {
        return `${postCategory.code} - ${postCategory.name}`;
    }

    updateSlug() {
        this.post.slug = SlugHelper.generateSlug(this.post.code);
    }
}
