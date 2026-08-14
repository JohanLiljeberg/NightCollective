/**
 * MemberFormHandler - Manages dynamic game contributions in member forms
 */
class MemberFormHandler {
    constructor(memberId, initialCount = 0) {
        this.memberId = memberId;
        this.currentIndex = initialCount;
        this.container = document.getElementById(`game-contributions-list-${memberId}`);
        this.addButton = document.querySelector(`[data-member-id="${memberId}"].add-game-contribution-btn`);

        if (!this.container) {
            console.error(`Container not found for member ID: ${memberId}`);
            return;
        }

        this.init();
    }

    init() {
        // Attach event listeners
        if (this.addButton) {
            this.addButton.addEventListener('click', () => this.addContribution());
        }

        // Delegate remove button clicks to container
        this.container.addEventListener('click', (e) => {
            if (e.target.closest('.remove-game-contribution-btn')) {
                this.removeContribution(e.target.closest('.remove-game-contribution-btn'));
            }
        });

        // Delegate game select change to update thumbnail preview
        this.container.addEventListener('change', (e) => {
            if (e.target.classList.contains('game-select')) {
                this.updateGamePreview(e.target);
            }
        });

        // Initialize previews for existing items
        this.container.querySelectorAll('.game-select').forEach(select => this.updateGamePreview(select));
    }

    /**
     * Show/hide the thumbnail preview next to a game select based on the selected option
     */
    updateGamePreview(select) {
        const preview = select.closest('.mb-3')?.querySelector('.game-select-preview');
        if (!preview) return;

        const selectedOption = select.options[select.selectedIndex];
        const imgUrl = selectedOption?.getAttribute('data-img');

        if (imgUrl) {
            preview.src = imgUrl;
            preview.style.display = '';
        } else {
            preview.style.display = 'none';
        }
    }

    /**
     * Add a new game contribution form item
     */
    addContribution() {
        const index = this.currentIndex++;
        const html = this.buildContributionHtml(index);
        this.container.insertAdjacentHTML('beforeend', html);
    }

    /**
     * Remove a contribution item and reindex remaining items
     */
    removeContribution(button) {
        const item = button.closest('.game-contribution-item');
        if (!item) return;

        // Bootstrap fade out animation
        item.classList.add('fade');
        setTimeout(() => {
            item.remove();
            this.reindexItems();
        }, 150);
    }

    /**
     * Reindex all contribution items after removal
     */
    reindexItems() {
        const items = this.container.querySelectorAll('.game-contribution-item');
        items.forEach((item, idx) => {
            const heading = item.querySelector('h6');
            if (heading) {
                heading.textContent = `Game Contribution #${idx + 1}`;
            }
        });
    }

    /**
     * Build HTML for a new contribution item
     */
    buildContributionHtml(index) {
        const gameOptions = this.getGameOptionsHtml();
        const workAreaCheckboxes = this.getWorkAreaCheckboxesHtml(index);

        return `
            <div class="game-contribution-item card mb-3" data-index="${index}">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-start mb-3">
                        <h6 class="mb-0">Game Contribution #${index + 1}</h6>
                        <button type="button" class="btn btn-sm btn-danger remove-game-contribution-btn" data-index="${index}">
                            <span aria-hidden="true">&times;</span> Remove
                        </button>
                    </div>

                    <div class="mb-3">
                        <label class="form-label" for="game-select-${this.memberId}-${index}">Game</label>
                        <div class="d-flex align-items-center gap-2">
                            <img class="game-select-preview rounded-circle border" width="32" height="32" style="object-fit: cover;" src="" alt="" />
                            <select class="form-select game-select" 
                                    id="game-select-${this.memberId}-${index}"
                                    name="MemberForm.GameContributions[${index}].GameId" 
                                    required>
                                <option value="">Select a game...</option>
                                ${gameOptions}
                            </select>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Involvement Level</label>
                        <div class="btn-group w-100" role="group" aria-label="Involvement level selection">
                            <input type="radio" 
                                   class="btn-check" 
                                   name="MemberForm.GameContributions[${index}].InvolvementLevel" 
                                   id="game-involvement-${this.memberId}-${index}-0" 
                                   value="0" 
                                   checked 
                                   autocomplete="off" />
                            <label class="btn btn-outline-primary" for="game-involvement-${this.memberId}-${index}-0">
                                🥉 Supporting
                            </label>

                            <input type="radio" 
                                   class="btn-check" 
                                   name="MemberForm.GameContributions[${index}].InvolvementLevel" 
                                   id="game-involvement-${this.memberId}-${index}-1" 
                                   value="1" 
                                   autocomplete="off" />
                            <label class="btn btn-outline-primary" for="game-involvement-${this.memberId}-${index}-1">
                                🥈 Major
                            </label>

                            <input type="radio" 
                                   class="btn-check" 
                                   name="MemberForm.GameContributions[${index}].InvolvementLevel" 
                                   id="game-involvement-${this.memberId}-${index}-2" 
                                   value="2" 
                                   autocomplete="off" />
                            <label class="btn btn-outline-primary" for="game-involvement-${this.memberId}-${index}-2">
                                🥇 Lead
                            </label>
                        </div>
                    </div>

                    <div class="mb-0">
                        <label class="form-label">Work Areas <small class="text-muted">(select all that apply)</small></label>
                        <div class="row g-2">
                            ${workAreaCheckboxes}
                        </div>
                    </div>
                </div>
            </div>
        `;
    }

    /**
     * Get game select optgroups/options HTML from an existing contribution select, falling back
     * to the hidden template select (used when there are zero existing contributions)
     */
    getGameOptionsHtml() {
        const existingSelect = this.container.querySelector('.form-select')
            ?? document.getElementById(`game-options-template-${this.memberId}`);

        if (existingSelect) {
            return Array.from(existingSelect.children)
                .map(child => {
                    if (child.tagName === 'OPTGROUP') {
                        const options = Array.from(child.children)
                            .filter(opt => opt.value !== '')
                            .map(opt => `<option value="${opt.value}" data-img="${opt.getAttribute('data-img') ?? ''}">${opt.text}</option>`)
                            .join('');
                        return `<optgroup label="${child.label}">${options}</optgroup>`;
                    }
                    if (child.tagName === 'OPTION' && child.value !== '') {
                        return `<option value="${child.value}" data-img="${child.getAttribute('data-img') ?? ''}">${child.text}</option>`;
                    }
                    return '';
                })
                .join('');
        }
        return '';
    }

    /**
     * Get work area checkboxes from the first existing item, falling back to the
     * hidden template (used when there are zero existing contributions)
     */
    getWorkAreaCheckboxesHtml(index) {
        const firstItem = this.container.querySelector('.game-contribution-item');
        const workAreaContainer = firstItem
            ? firstItem.querySelector('.row.g-2')
            : document.getElementById(`game-work-area-template-${this.memberId}`)?.querySelector('.row.g-2');

        if (!workAreaContainer) return '';

        // Clone and update the checkboxes for the new index
        const checkboxes = Array.from(workAreaContainer.querySelectorAll('.col-sm-6'));
        return checkboxes.map(col => {
            const checkbox = col.querySelector('input[type="checkbox"]');
            const label = col.querySelector('label');

            if (!checkbox || !label) return '';

            const value = checkbox.value;
            const labelText = label.textContent.trim();
            const newCheckboxId = `game-work-area-${this.memberId}-${index}-${value}`;

            return `
                <div class="col-sm-6">
                    <div class="form-check">
                        <input class="form-check-input" 
                               type="checkbox" 
                               name="MemberForm.GameContributions[${index}].SelectedWorkAreas" 
                               id="${newCheckboxId}" 
                               value="${value}" />
                        <label class="form-check-label" for="${newCheckboxId}">
                            ${labelText}
                        </label>
                    </div>
                </div>
            `;
        }).join('');
    }
}

// Make it available globally
window.MemberFormHandler = MemberFormHandler;
